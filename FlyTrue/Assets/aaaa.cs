using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaa : MonoBehaviour
{
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = gameObject.transform.rotation;
        transform.position = gameObject.transform.position;
    }
}
