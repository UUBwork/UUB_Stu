using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class LockHead : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject VR_Head;
    public GameObject Head;



    void Start()
    {
        InputTracking.disablePositionalTracking = true;


   
    }

    // Update is called once per frame
    void Update()
    {
        //VR_Head.transform.position = Head.transform.position;
    }


}
