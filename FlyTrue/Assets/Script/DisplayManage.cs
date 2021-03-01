using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayManage : MonoBehaviour
{
    public GameObject display1;


    public Transform target;
    public float x ;
    public float y ;
    public float xSpeed = 1;
    public float ySpeed = 1;
    public float distence  ;
    public float disSpeed = 1;
    public float minDisyence = 0.5f;
    public float maxDisyence = 1;
    public Quaternion rotationEuler;
    public Vector3 cameraPosition;
    public GameState _gameState;
    public GameObject playerTarge;
    private void Update()
    {
        UseButton();
        if (useMouse)
        {
            useMiuse();
        }
    }
    public bool isBig=false;
    void UseButton()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(display1.active)
                display1.active = false;
            else
                display1.active = true;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (!isBig)
            {
                display1.GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 1f);
                isBig = true;
            }
            else if (isBig)
            {
                display1.GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
                isBig = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            useMouse = true;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {

            _gameState.GameWait();
            playerTarge.transform.position =new Vector3(720,5,-2.37f);
        }

    }
    bool useMouse = false;
    void useMiuse()
    {


        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
      
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
       
        if (x > 360)
        {

            x -= 360;

        }
        else if (x < 0)
        {
            x += 360;

        }
        distence -= Input.GetAxis("Mouse ScrollWheel") * disSpeed * Time.deltaTime;
        distence = Mathf.Clamp(distence, minDisyence, maxDisyence);

        rotationEuler = Quaternion.Euler(y, x, 0);
        cameraPosition = rotationEuler * new Vector3(0, 0, -distence) + target.position;

        display1.transform.rotation = rotationEuler;
        display1.transform.position = cameraPosition;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
