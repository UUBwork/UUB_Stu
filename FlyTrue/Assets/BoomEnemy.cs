using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : MonoBehaviour
{

    EnemyValueControl _EnemyValueControl;
    public GameObject Boom;


    public int moveSpeed;
    public int rotationSpeed = 5;

    private Transform target;
    private Transform myTransform;
    private Vector3 targetPosition;
    private float maxDistance;

    public Animator Animator;
    
    public GameObject BoomRangeBall;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        _EnemyValueControl = this.GetComponent<EnemyValueControl>();
        _EnemyValueControl.SetValue(1, 1, 1);
        myTransform = transform;
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
            TrackState();
        }
    }

    void TrackState()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        Debug.DrawLine(target.position, myTransform.position, Color.red);


        targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetPosition - myTransform.position), rotationSpeed * Time.deltaTime);


        maxDistance = Vector3.Distance(targetPosition, myTransform.position);
        if (maxDistance >= 3)
        {

            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        if (maxDistance < 3)
        {
            Animator.SetBool("ATK", true);
            ATK();
        }


    }

    public bool isBoom = false;
    public void ATK()
    {
        

        if (!isBoom)
        {
            isBoom = true;
            GameObject N = Instantiate(BoomRangeBall, this.transform.position, this.transform.rotation);
            Dead();
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
        StartCoroutine("DelayTime");
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
        Dead();
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }


}
