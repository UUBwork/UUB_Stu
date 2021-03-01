using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Player2Input : MonoBehaviour {
    public bool Fire;
    public bool reFire;
    public bool skill1;
    public bool skill2;
    public bool skill3;
    public bool skill4;

    public bool leftBtn;
    public bool rigthBtn;

    SteamVR_TrackedObject TransfromObj;

    void Awake()
    {
        TransfromObj = GetComponent<SteamVR_TrackedObject>();
    }
    void FixedUpdate()
    {
       var device = SteamVR_Controller.Input((int)TransfromObj.index);
        //扳机键
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("你按下了板機键");
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("你按松开板機键");
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("你正按着板機键");
            Fire = true;
        }
        else
        {
            Fire = false;
        }


        //菜单键

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("你按下了菜单键");
        }

        //抓握按键手柄左右两侧的抓握按键为一个按键

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("你正紧握");
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("你松开了");
        }

        //下面是触摸板事件 我们通过判断坐标是否超过0.5来判断按了哪个

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.5f)
            {
                Debug.Log("你按下了触摸板的上");
                reFire = true;
            }
            else if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.5)
            {
                Debug.Log("你按下了触摸板的下");
            }

            if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.5f)
            {
                Debug.Log("你按下了触摸板的右");
                rigthBtn = true;
                Fire = true;
            }
            else if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.5f)
            {
                Debug.Log("你按下了触摸板的左");
                leftBtn = true;
                Fire = true;
            }
          

        }
          else
            {
                rigthBtn = false;
                leftBtn = false;
                reFire = false;
            Fire = false;
        }
     
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.5f)
            {
                Debug.Log("你触摸了触摸板的上");
            }
            else if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.5)
            {
                Debug.Log("你触摸了触摸板的下");
            }

            if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.5f)
            {
                Debug.Log("你触摸了触摸板的右");
            }
            else if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.5f)
            {
                Debug.Log("你触摸了触摸板的左");

            }
        }
        else
        {


        }

    }

}
