using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class gg3 : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grapAct;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(grapAct.GetState(handType));
    }
}
