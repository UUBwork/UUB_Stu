using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletValue : MonoBehaviour
{

    public int hp=1;
    public int Damage;

    public void SetDamage(int SetDamage)
    {
        Damage = SetDamage;
    }


    public int damagneInt()
    {
        return Damage;
    }
    public void SetHp(int sethp)
    {
        hp = sethp;
    }

    public void Hit(int damage)
    {
        hp = hp - damage;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
