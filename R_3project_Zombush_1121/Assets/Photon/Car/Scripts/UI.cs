using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : Photon.MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    // Use this for initialization
    void Start()
    {
        p1.active = false;
        p2.active = false;
        if (photonView.isMine)
        {
            p1.SetActive(true);
            p2.SetActive(false);

        }


        else
        {
            p1.SetActive(false);
            p2.SetActive(true);
        }

    }
    private void Awake()
    {
       
    }


    // Update is called once per frame
    void Update()
    {

    }
}
