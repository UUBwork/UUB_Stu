using UnityEngine;
using System.Collections;


public class LogitechSteeringWheelBasic : MonoBehaviour
{
   
   

    
    

    void Start()
    {
        
       // LogitechGSDK.LogiSteeringInitialize(false);
    }
    private void Update()
    {
      

        //aaa = LogitechGSDK.LogiGetStateUnity(0).lY;
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            print(LogitechGSDK.LogiGetStateUnity(0).lY);
        }
    }

}