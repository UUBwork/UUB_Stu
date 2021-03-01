using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userc : MonoBehaviour {

    public CarC m_CarC;
    public float v = 0;
    public float h = 0;
    public float handbreak=0;
	// Use this for initialization
	void Start () {
        m_CarC = GetComponent<CarC>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {



        Logitech();
        //aaa = LogitechGSDK.LogiGetStateUnity(0).lY;
        
        m_CarC.Move(h, v, v, handbreak);
        //m_CarC.Move(-1, 1, 1, handbreak);

    }

    void Logitech()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES aaa;
            aaa = LogitechGSDK.LogiGetStateUnity(0);

            LogitechGSDK.LogiPlayDamperForce(0, 50);

            if (aaa.rgbButtons[14] == 128)
            {
                v = Mathf.Abs(LogitechGSDK.LogiGetStateUnity(0).lY - 32767) / 65534.0f;
            }
            else if (aaa.rgbButtons[15] == 128)
            {

                v = Mathf.Abs(LogitechGSDK.LogiGetStateUnity(0).lY - 32767) / 65534.0f * -1;
            }
            else
            {
                v = 0;
            }


            if (LogitechGSDK.LogiGetStateUnity(0).lX > 0)
                h = Mathf.Abs(LogitechGSDK.LogiGetStateUnity(0).lX / 32767.0F);
            if (LogitechGSDK.LogiGetStateUnity(0).lX < 0)
                h = LogitechGSDK.LogiGetStateUnity(0).lX / (32768.0f);
            handbreak = Mathf.Abs(LogitechGSDK.LogiGetStateUnity(0).lRz - 32767);

            



        }
        //32767 -32768

    }
}
