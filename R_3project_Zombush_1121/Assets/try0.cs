using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class try0 : MonoBehaviour {
    public GameObject Enemy;

    // Use this for initialization
    void Start () {
        PhotonNetwork.Instantiate(this.Enemy.name, this.transform.position, Quaternion.identity, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

