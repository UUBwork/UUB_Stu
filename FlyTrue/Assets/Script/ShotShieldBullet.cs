using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotShieldBullet : MonoBehaviour
{


    public ShieldGun _ShieldGun;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
           // CollisionDamage(collider);
            Destroy(collider.gameObject);
        }
        if (collider.tag == "e_Bullet")
        {
            _ShieldGun.AccumulationDamage(1);
            Destroy(collider.gameObject);
        }




    }
    public void CollisionDamage(Collider collider)
    {

        DamageController.Damage(this.transform.gameObject, collider.gameObject, null);
       
    }
}
