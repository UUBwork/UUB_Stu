using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxmove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.L))
        transform.Translate(Time.deltaTime*1,0,0);
        if (Input.GetKey(KeyCode.K))
        transform.Translate(Time.deltaTime * -1, 0, 0);
    }
}
