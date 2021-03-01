using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartS : MonoBehaviour {

    SteamVR_TrackedObject TransfromObj;
    void Awake()
    {
        TransfromObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    private void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)TransfromObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("你按下了板機键");
            OnClick();
        }

        Logitech();

    }
    void Logitech()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES aaa;
            aaa = LogitechGSDK.LogiGetStateUnity(0);

            LogitechGSDK.LogiPlayDamperForce(0, 50);
           
            if (LogitechGSDK.LogiGetStateUnity(0).lY < 0)
                OnClick();

            if (Input.GetMouseButtonDown(0))
                OnClick();
        }
        

    }
    public void OnClick()
    {
        Application.LoadLevel("DemoWorker-Scene");
    }

}
