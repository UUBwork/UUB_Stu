using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZobWall : Photon.MonoBehaviour
{
    public GameObject BOSS;
    public WallMove _WallMove;
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

            //Destroy(other.gameObject);
            PhotonView photonView = PhotonView.Get(BOSS);
            photonView.RPC("BoomPlay", PhotonTargets.All);

            print("GameOvwr");
        }
        if (other.tag == "enemy")
        {
            Destroy(other.gameObject);
            _WallMove.speed++;
        }
    }

}
