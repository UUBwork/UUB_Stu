using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalQuality = 0; //水平
    public float VerticalQuality = 0; //垂直  
    public bool AttackBtn=false;      //攻擊按鍵
    public bool ReFilling = false;    //填裝按鍵

    public float _HorizontalQuality
    {
        get {
            if (HorizontalQuality > 1)
            {
                return 1;
            }
            else if (HorizontalQuality < -1)
            {
                return -1;
            }
            else
            {
                return HorizontalQuality;
            }
        }
        set { HorizontalQuality = value; }
    }
    public float _VerticalQuality
    {
        get { return VerticalQuality; }
        set { VerticalQuality = value; }
    }


}
