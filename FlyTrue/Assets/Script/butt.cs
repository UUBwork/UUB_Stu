using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butt : MonoBehaviour
{
    public GameObject topos;
    public Rigidbody rb;
    public Vector3 _Vector3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = _Vector3 * 5;
    }
    public void setPos(GameObject gameObject, GameObject G)
    {
        topos = gameObject;
        _Vector3 = gameObject.transform.position - G.transform.position;
}
}
