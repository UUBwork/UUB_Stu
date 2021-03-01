using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            CollisionDamage(collider);
            Destroy(this.gameObject);
      //      print("撞到人");
        }


        if (collider.tag == "Shield")
        {
            Destroy(this.gameObject);
            CollisionDamage(collider);
            
            //      print("撞到人");
        }


    }
    public void CollisionDamage(Collider collider)
    {

        DamageController.Damage(this.transform.gameObject, collider.gameObject, null);

    }
}
