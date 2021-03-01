using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNet : Photon.MonoBehaviour
{

    bool firstTake = false;

    private Vector3 correctEnemyPos = Vector3.zero; //We lerp towards this
    private Quaternion correctEnemyRot = Quaternion.identity; //We lerp towards this
    userc userc;
    e_AbilityValue _e_AbilityValue;

    ec _ec;

    void OnEnable()
    {
        firstTake = true;
    }

    void Awake()
    {
       
        _ec = GetComponent<ec>();
        _e_AbilityValue= GetComponent<e_AbilityValue>();

        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            
            _ec.enabled=true;
        }
        else
        {
          
            _ec.enabled= false;
            

        }

        gameObject.name = gameObject.name + photonView.viewID;
    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            
            //We own this player: send the others our data
            //stream.SendNext((int)controllerScript._characterState);
          //  stream.SendNext(transform.position);
         //   stream.SendNext(transform.rotation);

        //    stream.SendNext((int)_ec.m_state);

        


          

        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
         //   correctEnemyPos = (Vector3)stream.ReceiveNext();
         //   correctEnemyRot = (Quaternion)stream.ReceiveNext();

        //    _ec.m_state = (State)(int)stream.ReceiveNext();


            // avoids lerping the character from "center" to the "current" position when this client joins
            if (firstTake)
            {
              //  firstTake = false;
            //    this.transform.position = correctEnemyPos;
             //   transform.rotation = correctEnemyRot;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
          //  transform.position = Vector3.Lerp(transform.position, correctEnemyPos, Time.deltaTime);
         //   transform.rotation = Quaternion.Lerp(transform.rotation, correctEnemyRot, Time.deltaTime);
        }
    }
}
