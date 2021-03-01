using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMove : MonoBehaviour
{
    public Transform Targe;

    public float x ;
    public float z ;

    public float xSpeed = 20;
    public float zSpeed = 20;

    public float distence ;
   

    public float minDisyence ;
    public float maxDisyence ;


    public Quaternion rotationEuler;
    public Vector3 PlayerPos;

    // Start is called before the first frame update
    void Start()
    {
       

       

    }


    public AircraftMoveR _AircraftMoveR ;
    // Update is called once per frame
    void Update()
    {

        Open();


    }
    public bool OpevValue;
    public void Open()
    {
        if (OpevValue)
        {
            /*
            if (Input.GetKey(KeyCode.A))
            {
                x = x + xSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x = x - xSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.W) && z >= minDisyence + 1)
            {
                z = z - zSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) && z <= maxDisyence - 1)
            {
                z = z + zSpeed * Time.deltaTime;
            }
            */
            if (_AircraftMoveR.getMoveValue().z > 0)
            {
                x = x + xSpeed * _AircraftMoveR.getMoveValue().z  * Time.deltaTime;
            }
            if (_AircraftMoveR.getMoveValue().z < 0)
            {
                x = x + xSpeed * _AircraftMoveR.getMoveValue().z  * Time.deltaTime;
            }

            if (_AircraftMoveR.getMoveValue().x > 0 && z >= minDisyence + 1)
            {
                z = z - zSpeed * _AircraftMoveR.getMoveValue().x  * Time.deltaTime;
            }
            if (_AircraftMoveR.getMoveValue().x < 0 && z <= maxDisyence - 1)
            {
                z = z - zSpeed * _AircraftMoveR.getMoveValue().x  * Time.deltaTime;
            }
        
            
            check();
        }
    }
   

    void lockMove()
    {


        distence = Mathf.Clamp(z, minDisyence, maxDisyence);
        
        rotationEuler =  Quaternion.Euler(0, x-180, 0);

        PlayerPos = rotationEuler * new Vector3(0, 0, -distence) + Targe.position;

        transform.rotation = rotationEuler ;
        
        transform.position = PlayerPos;
    }


    private Transform myTransform;
    private Vector3 targetPosition;
    private float maxDistance;

    bool F = true;
    void check()
    {
        myTransform = transform;
        targetPosition = new Vector3(Targe.position.x, Targe.position.y, Targe.position.z);

        maxDistance = Vector3.Distance(targetPosition, myTransform.position);
        if (maxDistance <= maxDisyence)
        {
           
            if (F)
            {
                z = maxDistance;
                
                Vector3 relative = transform.InverseTransformPoint(Targe.transform.position);
                x = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                F = false;
               
            }
            lockMove();

           
        }
        else
        {
            
           

        }

    }
   
   
}
