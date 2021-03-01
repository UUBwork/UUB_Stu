using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shotFire;
    public float shootForce = 0f;
    public GameObject ShotP;
    public GameObject Blood;
    float timer;
    public float sec;
 
   
    public void GunParticle()
    {
        timer += Time.deltaTime;
        if (timer >= sec)
        {
         //   GameObject projectile = (GameObject)PhotonNetwork.Instantiate(bullet.name, ShotP.transform.position, ShotP.transform.rotation, 0);

        //    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
         //   PhotonNetwork.Instantiate(shotFire.name, ShotP.transform.position, ShotP.transform.rotation, 0);
            timer = 0;
            print("GunParticle");
        }
    }
  //  [PunRPC]
    public void blood(Collider enemy)
    {
    //    PhotonNetwork.Instantiate(Blood.name, enemy.transform.position, Quaternion.identity, 0);
        print("BLOOD");
    }
}
