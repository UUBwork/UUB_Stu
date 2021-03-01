using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Enemy : MonoBehaviour
{
    

    public int Hp = 3;
    EnemyValueControl _EnemyValueControl;
    public GameObject Boom;
    // Start is called before the first frame update
    void Start()
    {
        _EnemyValueControl = this.GetComponent<EnemyValueControl>();
        _EnemyValueControl.SetValue(Hp, Hp, 1);
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
            UFOState();
        }
       
    }
    void UFOState()
    {

        if (this.gameObject.transform.position.y <= 5)
        {

        }
        else
        {
            this.transform.position -= new Vector3(0, 0.05f, 0);
        }

        this.transform.position -= new Vector3(0.04f, 0, 0);

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
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

}
