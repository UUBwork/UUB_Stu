using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackeR : MonoBehaviour
{
    public Quaternion originalR;
    public Vector3 originalP;
    public bool First=true;


    private Quaternion _CurrentGyro;
    private Quaternion _originalRInverse;
    private Vector3 _Pointer;

    // Start is called before the first frame update
    void Awake()
    {
        originalR = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        _CurrentGyro = transform.rotation;
        if (transform.rotation.x!=0&& First)
        {
            First = false;
            originalR = transform.rotation;
            _originalRInverse = Quaternion.Inverse(originalR);
            _Pointer = Vector3.forward;
            originalP = transform.rotation.eulerAngles;
        }
       
        // print(transform.rotation.eulerAngles- originalR.eulerAngles);
        //print(((Vector3)transform.rotation.eulerAngles - (Vector3)originalR.eulerAngles));
        // print(_Pointer);
    }
}
