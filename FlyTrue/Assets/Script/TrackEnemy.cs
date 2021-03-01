using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnemy : MonoBehaviour
{


    EnemyValueControl _EnemyValueControl;
    public GameObject Boom;


    public int moveSpeed ;
    public int rotationSpeed = 5;

    private Transform target;
    private Transform myTransform;
    private Vector3 targetPosition;
    private float maxDistance;




    // Start is called before the first frame update
    void Start()
    {
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
        if (maxDistance >= 0.5F)
        {
            
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
           
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
        print(this.name);
        Dead();
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

}
