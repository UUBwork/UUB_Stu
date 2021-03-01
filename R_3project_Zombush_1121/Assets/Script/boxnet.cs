using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boxnet : Photon.MonoBehaviour
{
    bool firstTake = false;

    private Vector3 correctBoxPos = Vector3.zero; //We lerp towards this
    private Quaternion correctBoxRot = Quaternion.identity; //We lerp towards this
    boxmove boxmove;

    void OnEnable()
    {
        firstTake = true;
    }

    void Awake()
    {
        boxmove = GetComponent<boxmove>();
        

        if (!photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            boxmove.enabled = true;
            
        }
        else
        {
            boxmove.enabled = false;
           

        }

        gameObject.name = gameObject.name + photonView.viewID;
    }

   

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            //stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);


        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctBoxPos = (Vector3)stream.ReceiveNext();
            correctBoxRot = (Quaternion)stream.ReceiveNext();

            // avoids lerping the character from "center" to the "current" position when this client joins
            if (firstTake)
            {
                firstTake = false;
                this.transform.position = correctBoxPos;
                transform.rotation = correctBoxRot;
            }

        }
    }

  
	
	// Update is called once per frame
	void Update () {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctBoxPos, Time.deltaTime) ;
            transform.rotation = Quaternion.Lerp(transform.rotation, correctBoxRot, Time.deltaTime );
        }
    }
}

