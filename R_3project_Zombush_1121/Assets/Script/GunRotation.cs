using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {

    public GameObject _SteamVR_TrackedObject;
    

    void FixedUpdate()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(_SteamVR_TrackedObject.transform.rotation.eulerAngles.x-30, _SteamVR_TrackedObject.transform.rotation.eulerAngles.y, 0));
       
        //Debug.Log(_SteamVR_TrackedObject.transform.rotation.eulerAngles);
    } 
}
