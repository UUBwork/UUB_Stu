using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : Photon.MonoBehaviour
{
    public Transform Enemy;
    public Transform Targe;
    public int EnemyInt = 0;
    public Vector3 textV;
    // Use this for initialization
    void Awake () {
        //  PhotonNetwork.Instantiate(this.Enemy.name, Targe.transform.position, Quaternion.identity, 0);
        //StartCoroutine("DeathDelay");
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts

            this.enabled = true;
        }
        else
        {

            this.enabled = false;


        }
        InvokeRepeating("ADD", 10, 30);
    }
	
	// Update is called once per frame
	void Update () {

        textV.x = Random.Range(-10.0f, 10.0f);
        textV.y = 0;
        textV.z = Random.Range(-10.0f, 10.0f);

    }

    
    void ADD()
    {
        if (EnemyInt <= 3)
        {
            PhotonNetwork.Instantiate(this.Enemy.name, this.transform.position+ textV, Quaternion.identity, 0);
            EnemyInt++;
        }
    }

}
