using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XLG : MonoBehaviour
{
    public BakeEnemy _bakeEnemy;
    public BossEnemy _bossEnemy;
    float nextTime=0.5f;
    float nextDamageTime = 0f;
   
    private void OnParticleCollision(GameObject other)
    {
        
           
        
        if (Time.time > nextDamageTime)
        {
            nextDamageTime = Time.time + nextTime;
            SendMessage(other);
        }


    }
    void SendMessage(GameObject other)
    {
        try
        {
          
            _bossEnemy.GunDamage(this.gameObject, other);
        }
        catch
        {
           
            _bakeEnemy.GunDamage(this.gameObject, other);
        }
     //   print(other.name);
        
    }

}
