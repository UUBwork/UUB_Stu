using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {

        if (other.collider.tag == "Player")
        {

            print("Player");
            other.collider.GetComponent<c_AbilityValue>().HP = other.collider.GetComponent<c_AbilityValue>().HP - 1;
        }
        Destroy(this.gameObject);

    }
}
