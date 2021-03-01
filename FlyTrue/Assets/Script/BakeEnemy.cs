using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeEnemy : MonoBehaviour
{
    EnemyValueControl _EnemyValueControl;

    public GameObject Boom;

    public int moveSpeed;
    public int rotationSpeed = 5;

    private Transform target;
    private Transform myTransform;
    private Vector3 targetPosition;
    private float maxDistance;


    public GameObject Gun;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;
    public AudioSource _AudioSource;

    int count=0;

    public enum BakeState
    {
        Dead,
        Attack,
        Move,

        RunAway,
    }
    public BakeState _BakeState;

    void Start()
    {
       
        _EnemyValueControl = this.GetComponent<EnemyValueControl>();
        _AudioSource = this.GetComponent<AudioSource>();
        _BakeState = BakeState.Move;


        _EnemyValueControl.SetValue(2, 2, 1);
        myTransform = transform;

        gunParticles = Gun.GetComponent<ParticleSystem>();
        gunLine = Gun.GetComponent<LineRenderer>();
        gunAudio = Gun.GetComponent<AudioSource>();
        gunLight = Gun.GetComponent<Light>();
        gunParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(this.gameObject.name + " hp  " + _enemyValue.HP);
        if (_EnemyValueControl.NowHP() <= 0)
        {
            Dead();
        }
        if (_EnemyValueControl.NowHP() > 0)
        {
            SwitchState();
        }

    }


    void SwitchState()
    {
        switch (_BakeState)
        {

            case BakeState.Move:
                Move();
                break;
            case BakeState.Attack:
                Attack();
                break;
            case BakeState.Dead:
                Dead();
                break;
            case BakeState.RunAway:
                RunAway();
                break;
            default:

                break;
        }
    }


    
   
    void RunAway()
    {
        _BakeState = BakeState.RunAway;
        myTransform.position += -myTransform.forward * moveSpeed * Time.deltaTime;

        StartCoroutine("DelayTime",3);

    }
 
    
    void Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        Debug.DrawLine(target.position, myTransform.position, Color.red);


        targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetPosition - myTransform.position), rotationSpeed * Time.deltaTime);


        maxDistance = Vector3.Distance(targetPosition, myTransform.position);
        if (maxDistance >= 10)
        {

            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {


           _BakeState = BakeState.Attack;
        }
    }
    void Attack()
    {
        _AudioSource.Play();
        if (gunParticles.isStopped)
        {
            if (count == 0)
            {
                gunParticles.Play();
                count++;
            }
            else
            {
                Invoke("RunAway",2);
            }

        }
       
    }


   
    bool IsBoom = true;

    public void Dead()
    {
        if (IsBoom)
        {
            Instantiate(Boom, this.transform.position, this.transform.rotation, this.gameObject.transform);
            IsBoom = false;
        }
        StartCoroutine("DelayTime",1f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            CollisionDamage(collider);

        }
        if (collider.tag == "DeadLine")
        {
            Destroy(this.gameObject);
        }




    }
    public void CollisionDamage(Collider collider)
    {

        DamageController.Damage(this.transform.gameObject, collider.gameObject, null);
       
    }
    public void GunDamage(GameObject attacker,GameObject collider)
    {
        DamageController.Damage(attacker, collider, null);
    }

    IEnumerator DelayTime(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
