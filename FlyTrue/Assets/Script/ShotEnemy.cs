using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{

    EnemyValueControl _EnemyValueControl;

    public GameObject Boom;

    public int moveSpeed;
    public int rotationSpeed = 5;

    private Transform target;
    private Transform myTransform;
    private Vector3 targetPosition;
    private float maxDistance;

    public GameObject bullet;
    public GameObject Player;
    public GameObject aimsTarge;
    public GameObject ShotPos_r;
    public GameObject ShotPos_l;


    public GameObject PlayerTager;
    int count = 0;
    public AudioSource _audioSource;

    public enum BakeState
    {
        Dead,
        Attack,
        Move,
        RunAway,
    }
    public BakeState _BakeState;
    // Start is called before the first frame update
    void Start()
    {
        _EnemyValueControl = this.GetComponent<EnemyValueControl>();
        _audioSource = this.GetComponent<AudioSource>();
        _BakeState = BakeState.Move;


        _EnemyValueControl.SetValue(1, 1, 1);
        myTransform = transform;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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

        StartCoroutine("DelayTime", 3);

    }


    void Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        Debug.DrawLine(target.position, myTransform.position, Color.red);


        targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetPosition - myTransform.position), rotationSpeed * Time.deltaTime);


        maxDistance = Vector3.Distance(targetPosition, myTransform.position);
        if (maxDistance >= 20)
        {

            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {


            _BakeState = BakeState.Attack;
        }
    }

    bool Executed=true;
    void Attack()
    {

        if (Executed)
        {
            InvokeRepeating("SinglePointShot", 1, 1);
            Executed = false;   
        }
    }



    void reState()
    {
        counter = 0;
    }

    public int counter = 0;
    GameObject G;
    void SinglePointShot()
    {
        if (counter < 3)
        {
            _audioSource.Play();
            counter++;

            G = Instantiate(PlayerTager, Player.transform.position, this.transform.rotation);

            GameObject N = Instantiate(bullet, ShotPos_r.transform.position, this.transform.rotation);
            N.GetComponent<Rigidbody>().AddForce(N.transform.forward);
            N.GetComponent<butt>().setPos(G, this.gameObject);

            GameObject Na = Instantiate(bullet, ShotPos_l.transform.position, this.transform.rotation);
            Na.GetComponent<Rigidbody>().AddForce(Na.transform.forward);
            Na.GetComponent<butt>().setPos(G, this.gameObject);
        }
        else
        {
            CancelInvoke("Attack");
            reState();
            
            RunAway();

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
        StartCoroutine("DelayTime", 1f);
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
    public void GunDamage(GameObject attacker, GameObject collider)
    {
        DamageController.Damage(attacker, collider, null);
    }

    IEnumerator DelayTime(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }


}
