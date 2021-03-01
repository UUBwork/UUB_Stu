using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DamageController
{

    public static void Damage(GameObject attacker, GameObject target, GameObject collision)
    {

        if (attacker.tag=="Player" )
        {
            if (target.tag == "Enemy")
            {
                Debug.Log(attacker.GetComponent<GunManage>().damagneInt());

                target.GetComponent<EnemyValueControl>().Hit(attacker.GetComponent<GunManage>().damagneInt());

                Debug.Log(target.GetComponent<EnemyValueControl>().NowHP());
            }
            if (target.tag == "e_Buttle")
            {
                target.GetComponent<BulletValue>().Hit(attacker.GetComponent<GunManage>().damagneInt());
            }
        }


        if (attacker.tag == "Player")
        {
            if (target.tag == "Boss")
            {
               
                target.GetComponent<EnemyValueControl>().Hit(attacker.GetComponent<GunManage>().damagneInt());
                target.GetComponent<BossEnemy>().Hit();

            }
            if (target.tag == "e_Buttle")
            {
                target.GetComponent<BulletValue>().Hit(attacker.GetComponent<GunManage>().damagneInt());
            }
        }



        if (attacker.tag == "p_Buttle" && target.tag == "Enemy")
        {


            

            target.GetComponent<EnemyValueControl>().Hit(attacker.GetComponent<BulletValue>().damagneInt());

          


        }

        if (attacker.tag == "e_Buttle"  )
        {
            if (target.tag == "Shield")
            {
                target.GetComponent<ShieldGun>().AccumulationDamage(attacker.GetComponent<BulletValue>().damagneInt());
                Debug.Log("aaa");
                
            }

            if (target.tag == "Player")
            {
                Debug.Log(target.GetComponent<Player>().GetHP());
                target.GetComponent<Player>().Hit(attacker.GetComponent<BulletValue>().damagneInt());
                Debug.Log(target.GetComponent<Player>().GetHP());
            }
            



        }
    

        if (attacker.tag == "Enemy" && target.tag == "Player")
        {
            target.GetComponent<Player>().Hit(attacker.GetComponent<EnemyValueControl>().Atk());
        }

        if (target.tag == "UI")
        {
            if (target.name == "Start")
            {
                //target.GetComponent<>();
                target.GetComponent<btn_Start>().GoMain();

            }
            if (target.name=="Exit")
            {
                Application.Quit();
            }
            if (target.name == "StartBOSS")
            {
                target.GetComponent<btn_Start>().GoBossStart();
            }
            if (target.name == "RESTART")
            {
                SceneManager.LoadScene(0);
            }

        }


    }




}
