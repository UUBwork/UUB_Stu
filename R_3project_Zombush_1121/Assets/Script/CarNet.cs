using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;



public class CarNet : Photon.MonoBehaviour
{
    public GameObject BoomPoint;
    bool firstTake = false;
    public GameObject BOOM;
    public bool BOOMFirst=true;
    private Vector3 correctBoxPos = Vector3.zero; //We lerp towards this
    private Quaternion correctBoxRot = Quaternion.identity; //We lerp towards this
    userc userc;
    c_AbilityValue _c_AbilityValue;
    CarC _CarC;
    Rigidbody _Rigidbody;
    void OnEnable()
    {
        firstTake = true;
    }

    void Awake()
    {
        _CarC = GetComponent<CarC>();
        userc = GetComponent<userc>();
        _c_AbilityValue = GetComponent<c_AbilityValue>();
        _Rigidbody = GetComponent<Rigidbody>();
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            userc.enabled = true;
            _CarC.enabled = true;
        }
        else
        {
            userc.enabled = false;
            _CarC.enabled = false;
            _Rigidbody.isKinematic = true;
        }

        gameObject.name = gameObject.name + photonView.viewID;
    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       /* if (stream.isWriting)
        {
            //We own this player: send the others our data
            //stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(_c_AbilityValue.HP);

        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctBoxPos = (Vector3)stream.ReceiveNext();
            correctBoxRot = (Quaternion)stream.ReceiveNext();
            _c_AbilityValue.HP=(int)stream.ReceiveNext();
            // avoids lerping the character from "center" to the "current" position when this client joins
            if (firstTake)
            {
                firstTake = false;
                this.transform.position = correctBoxPos;
                transform.rotation = correctBoxRot;
            }

        }*/
    }

    [PunRPC]
    void CarDestroy()
    {
        print("死亡網路傳");
        //SceneManager.LoadScene("DemoWorker - Scene");
        Application.LoadLevel("DemoWorker-Scene");
        Destroy(this.gameObject);
        
    }



    [PunRPC]
    void TrapTrigger()
    {
        //_CarC.CurrentSpeed = _CarC.CurrentSpeed;
     
        print("撞到陷阱");

    }
    [PunRPC]
    void BoomPlay()
    {
        //_CarC.CurrentSpeed = _CarC.CurrentSpeed;
        if (BOOMFirst)
        {
            PhotonNetwork.Instantiate(BOOM.name, BoomPoint.transform.position, Quaternion.identity, 0);
            StartCoroutine("DeathDelay");
            BOOMFirst = false;
        }
          
        print("123");
    }

    [PunRPC]
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2.0f);
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("CarDestroy", PhotonTargets.All);
    }


    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
         //   transform.position = Vector3.Lerp(transform.position, correctBoxPos, Time.deltaTime);
          //  transform.rotation = Quaternion.Lerp(transform.rotation, correctBoxRot, Time.deltaTime);
        }
    }
}

