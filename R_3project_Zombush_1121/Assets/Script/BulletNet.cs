using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNet : Photon.MonoBehaviour
{

    private Vector3 correctBulletPos = Vector3.zero; //We lerp towards this
    private Quaternion correctBulletRot = Quaternion.identity; //We lerp towards this

    void OnEnable()
    {
        
    }

    void Awake()
    {
        
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts

           
        }
        else
        {

          


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
            correctBulletPos = (Vector3)stream.ReceiveNext();
            correctBulletRot = (Quaternion)stream.ReceiveNext();

           // _ec.m_state = (State)(int)stream.ReceiveNext();


           

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctBulletPos, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctBulletRot, Time.deltaTime);
        }
    }
}
