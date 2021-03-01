using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotationNet : Photon.MonoBehaviour
{

    bool firstTake = false;

    private Vector3 correctGunPos = Vector3.zero; //We lerp towards this
    private Quaternion correctGunRot = Quaternion.identity; //We lerp towards this

    public GunRotation _GunRotation;

    void OnEnable()
    {
        firstTake = true;
    }

    void Awake()
    {
        _GunRotation = GetComponent<GunRotation>();
        photonView.TransferOwnership(2);
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            _GunRotation.enabled = true;
            //_ec.enabled=true;
        }
        else
        {

            // _ec.enabled=true;
            _GunRotation.enabled = false;
           // photonView.TransferOwnership(2);

        }

        gameObject.name = gameObject.name + photonView.viewID;
    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {

            //We own this player: send the others our data
            //stream.SendNext((int)controllerScript._characterState);
         //   stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            

        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
          //  correctGunPos = (Vector3)stream.ReceiveNext();
           correctGunRot = (Quaternion)stream.ReceiveNext();
            
            // avoids lerping the character from "center" to the "current" position when this client joins
            if (firstTake)
            {
                firstTake = false;
             //   this.transform.position = correctGunPos;
             //   transform.rotation = correctGunRot;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            _GunRotation.enabled = true;
            //_ec.enabled=true;
        }
        else
        {

            // _ec.enabled=true;
            _GunRotation.enabled = false;
            // photonView.TransferOwnership(2);

        }
        

        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)

        //    transform.position = Vector3.Lerp(transform.position, correctGunPos, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctGunRot, Time.deltaTime);
        }
        
    }
}
