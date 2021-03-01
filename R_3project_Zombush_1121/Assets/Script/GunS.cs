using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunS : MonoBehaviour
{

    public float nextShotTime;
    public int ammoCount;
    public float shotDelay = 0.15f;
    public float fireRate = 0.15f;
    public float reloadDelay;
    public int magazineSize=0;
    public int magazineMaxSize=200;
    public Transform muzzleTransform;

   
    public float Atk = 1;
    public float text = 1;
    public Vector3 textV;

    public Player2Input _Player2Input;
    public Vector3 shotPos;

    public LineRenderer laserLine;
    public Gun _Gun;
    // Use this for initialization
    void Start()
    {
       // laserLine = GetComponent<LineRenderer>();
        shotPos = muzzleTransform.position;
        magazineMaxSize = 200;
        magazineSize = magazineMaxSize;
    }

    // Update is called once per frame
    void Update()
    {
        shotPos = muzzleTransform.position;
        
        /*laserLine.SetPosition(0, shotPos);

        textV.x = Random.Range(0.0f, 10.0f);
        textV.y = Random.Range(0.0f, 10.0f);
        textV.z = Random.Range(0.0f, 10.0f);*/
        RaycastHit hit;
        Ray ray = new Ray(muzzleTransform.transform.position, muzzleTransform.transform.forward * 100);
        if (_Player2Input.Fire && Time.time > shotDelay && magazineSize > 0)
        {
            magazineSize--;
            //StartCoroutine(ShotEffect());
            shotPos = muzzleTransform.position;

            shotDelay = Time.time + fireRate;
            textV.x = Random.Range(0.0f, 10.0f);
            textV.y = Random.Range(0.0f, 10.0f);
            textV.z = Random.Range(0.0f, 10.0f);
            
            

            PhotonView g_photonView = PhotonView.Get(GetComponent<Gun>());
            g_photonView.RPC("GunParticle", PhotonTargets.All);

         /*   if (Physics.Raycast(muzzleTransform.transform.position, muzzleTransform.transform.forward * 1000+textV, out hit))
            {

               // Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "enemy")
                {

                    PhotonView e_photonView = PhotonView.Get(hit.collider.GetComponent<ec>());
                    e_photonView.RPC("DamageEnemy", PhotonTargets.All);

                    g_photonView.RPC("blood", PhotonTargets.All, hit.collider);
                
                    //hit.collider.GetComponent<ec>().e_AbilityValue.HP= hit.collider.GetComponent<ec>().e_AbilityValue.HP-Atk;
                    //print(hit.collider.GetComponent<ec>().e_AbilityValue.HP);

                }
                if (hit.collider.tag == "Boss")
                {

                    PhotonView e_photonView = PhotonView.Get(hit.collider.GetComponent<BossC>());
                    e_photonView.RPC("DamageBoss", PhotonTargets.All);

                    g_photonView.RPC("blood", PhotonTargets.All, hit.collider);

                    //hit.collider.GetComponent<ec>().e_AbilityValue.HP= hit.collider.GetComponent<ec>().e_AbilityValue.HP-Atk;
                    //print(hit.collider.GetComponent<ec>().e_AbilityValue.HP);

                }
                */
                //laserLine.SetPosition(1, hit.point);



            }
        //laserLine.SetPosition(1, muzzleTransform.forward*100);
        // Debug.DrawLine(muzzleTransform.transform.position, muzzleTransform.transform.position + muzzleTransform.transform.forward * 100 + textV, Color.red);

        if (_Player2Input.reFire)
        {
            magazineSize = magazineMaxSize;


        }

    }



    


    IEnumerator ShotEffect()
    {

        // 显示射击轨迹
        laserLine.enabled = true;

        // 等待0.07秒
        yield return new WaitForSeconds(0.005f);

        // 等待结束后隐藏轨迹
        laserLine.enabled = false;
    }

}


