using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftMoveR : MonoBehaviour
{
    public GameObject Tracker;

    bool First=true;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private Quaternion _CurrentGyro;
    private Quaternion _OriginGyro;
    private Quaternion _OriginGyroInverse;
    private Vector3 _Pointer;
    public Vector3 MoveValue;
    void Update()
    {
   
        if (First)
        {
            _CurrentGyro = Tracker.transform.rotation;
            _OriginGyroInverse= Quaternion.Inverse(_OriginGyro);
            _Pointer = Vector3.forward;
            First = false;
        }
        _CurrentGyro = Tracker.transform.rotation;
        _Pointer = _CurrentGyro * Vector3.forward;
       
        //Debug.Log("The Device is pointing: " + _OriginGyroInverse * _Pointer);
        MoveValue = _OriginGyroInverse * _Pointer;
    //+X 往前 -X往後 -Z右邊 +Z左邊

}
    public Vector3 getMoveValue()
    {
        return MoveValue;
    }
}
