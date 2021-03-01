using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{

    public Collider floot;
    public bool isCreat = true;
     void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player"&& isCreat)
        {
            Copy();
            
        }
        if (collider.tag == "Player" && !isCreat)
        {
            Del();
            
        }



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isCreat)
            {
                isCreat = false;
            }
            else
            {
                isCreat = true;
            }
        }
    }

    void Copy()
    {
        float length = floot.bounds.extents.x*2;
        Vector3 pos = transform.position - new Vector3(length, 0, 0);
        Instantiate(gameObject, pos, transform.rotation);


    }
    void Del()
    {
        Destroy(this.gameObject);
    }

}