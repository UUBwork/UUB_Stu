using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class core : MonoBehaviour
{
    public float rayL = 0.001f;
    RaycastHit hit;
    public LayerMask wallLayer;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // print(transform.right);
       // print(Vector3.Angle(Quaternion.Euler(0, 45, 0)*transform.right, Quaternion.Euler(0, -45, 0) *- transform.right ));


        transform.position = transform.position  -transform.forward*Time.deltaTime*5;

        // Debug.Log(R_RaycastHit() + "    " + L_RaycastHit());
        //Debug.Log(RO_RaycastHit() + "    " + LO_RaycastHit());
        if (Mathf.Round(RO_RaycastHit() * 10) != Mathf.Round(LO_RaycastHit() * 10))
        {
            if (Mathf.Round(RO_RaycastHit() * 10) >= Mathf.Round(LO_RaycastHit() * 10))
            {
                transform.Rotate(0, -1, 0);
            }
            if (Mathf.Round(RO_RaycastHit() * 10) < Mathf.Round(LO_RaycastHit() * 10))
            {
                transform.Rotate(0, 1, 0);
            }
            
        }
        else
        {
            if (Mathf.Round(R_RaycastHit() * 10) >= Mathf.Round(L_RaycastHit() * 10))
            {
                transform.Translate(transform.right * Time.deltaTime * 0.1F, Space.World);
            }
            else if (Mathf.Round(R_RaycastHit() * 10) < Mathf.Round(L_RaycastHit() * 10))
            {
                transform.Translate(-transform.right * Time.deltaTime * 0.1F, Space.World);

            }
        }
        print("UP"+TOP_RaycastHit());
        if (TOP_RaycastHit()<1.5f)
        {
            transform.Translate(transform.up * Time.deltaTime * 0.5F, Space.World);

            

        }
        else
        {

        }
         //Debug.Log(Mathf.Round(RO_RaycastHit()*10) + " :" + Mathf.Round(LO_RaycastHit() * 10));
         


        //  Debug.DrawRay(transform.position, Vector3.down);
    }


    void StartRay()
    {

    }

    float R_RaycastHit()
    {
        RaycastHit hit;
        float dis = 0 ;
        if (Physics.Raycast(transform.position, transform.right, out hit, rayL, wallLayer))
        {
            
            
            dis = Vector3.Distance(transform.position, hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.red, 0.01f, false);
        }
        
        return dis;
    }
    float L_RaycastHit()
    {
        float dis = 0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right*-1 , out hit, rayL, wallLayer))
        {

            dis = Vector3.Distance(transform.position, hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.green, 0.01f, false);
        }
       
        return dis;
    }


    float RO_RaycastHit()
    {
        RaycastHit hit;
        float dis = 0;
        if (Physics.Raycast(transform.position, Quaternion.Euler(0, 45, 0)*transform.right, out hit, rayL, wallLayer))
        {


            dis = Vector3.Distance(transform.position, hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.blue, 0.01f, false);
        }
       
        return dis;
    }
    float LO_RaycastHit()
    {
        RaycastHit hit;
        float dis = 0;
        
        if (Physics.Raycast(transform.position, Quaternion.Euler(0, -45, 0) * -transform.right, out hit, rayL, wallLayer))
        {


            dis = Vector3.Distance(transform.position, hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.grey, 0.01f, false);
        }
       
        return dis;
    }

    float TOP_RaycastHit()
    {
        RaycastHit hit;
        float dis = 0;

        if (Physics.Raycast(transform.position, Quaternion.Euler(0, 0, 0) * -transform.up, out hit, rayL, wallLayer))
        {


            dis = Vector3.Distance(transform.position, hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.red, 0.01f, false);
        }

        return dis;
    }
}
