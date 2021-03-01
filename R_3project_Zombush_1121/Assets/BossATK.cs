using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossATK : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            print("Player");
            other.GetComponent<c_AbilityValue>().HP = other.GetComponent<c_AbilityValue>().HP - 10;
           // PhotonView photonView = PhotonView.Get(other.GetComponent<c_AbilityValue>());
           // photonView.RPC("CarDamage", PhotonTargets.All, 10);
        }
        print("12");


    }

}
