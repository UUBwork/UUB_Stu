using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;


public class List : MonoBehaviour {

    
    public int ID = 2;
    public int min = 0;
    public int max = 3;
    public RectTransform list;
    public Player2Input _Player2Input;
    // Use this for initialization


    SteamVR_TrackedObject TransfromObj;
    void Awake()
    {
        
    }

    private void FixedUpdate()
    {
    

            if (_Player2Input.Fire)
            {
                Debug.Log("你按下了板機键");
                GoMap();
            }

            if (_Player2Input.leftBtn)
            {
                Debug.Log("你按下了左板機键");
                last();
            }

            if (_Player2Input.rigthBtn)
            {
                Debug.Log("你按下了右板機键");
                next();
            }

            if (Input.GetMouseButtonDown(0))
            {
                GoMap();
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

            
            if (LogitechGSDK.LogiButtonTriggered(0,4))
            {
                last();
               
            }


        
            if (LogitechGSDK.LogiButtonTriggered(0, 5))
            {
                next();
                
            }

            if (LogitechGSDK.LogiGetStateUnity(0).lY < 0)
            {
                GoMap();
            }
              

          
               
        }


    }



    public void next()
    {
        if (ID > min&&ID<=max)
        {
            list.anchoredPosition3D = list.anchoredPosition3D+ new Vector3(-100,0,0);
            ID--;

        }
    }

    public void last()
    {
        if (ID >= min && ID < max)
        {
            list.anchoredPosition3D = list.anchoredPosition3D + new Vector3(100, 0, 0);
            ID++;

        }
    }

    public void GoMap()
    {

        if (ID == 2)
        {
            //Application.LoadLevel("");
            print("去地圖２");
        }
        print("gomap");
    }


}
