using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float x = 358;
    public float y = 27;
    public float xSpeed = 20;
    public float ySpeed = 10;
    public float distence = 1;
    public float disSpeed = 1;
    public float minDisyence = 5;
    public float maxDisyence = 5;

    public float Xmove;

    public Quaternion rotationEuler;
    public Vector3 cameraPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (x >= 500)
            Xmove = -0.2f;
        if (x <= 420)
            Xmove = 0.2f;

        x = x + Xmove+xSpeed * Time.deltaTime* Xmove;
        

        distence = Mathf.Clamp(distence, minDisyence, maxDisyence);

        rotationEuler = Quaternion.Euler(y, x, 0);
        cameraPosition = rotationEuler * new Vector3(0, 0, -distence) + target.position;

        transform.rotation = rotationEuler;
        transform.position = cameraPosition;
        
    }

}
