using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNet : Photon.MonoBehaviour
{

    bool firstTake = false;

    private Vector3 correctBossPos = Vector3.zero; //We lerp towards this
    private Quaternion correctBossRot = Quaternion.identity; //We lerp towards this
    public BossMove _BossMove;
    public BossC _BossC;
    void OnEnable()
    {
        firstTake = true;
    }

    void Awake()
    {

        _BossC = GetComponent<BossC>();
        //_BossC.HP =20;
        _BossMove = GetComponent<BossMove>();

        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            _BossC.enabled = true;
            _BossMove.enabled = true;
        }
        else
        {
            _BossC.enabled = true;
            _BossMove.enabled = false;


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

            stream.SendNext((int)_BossC.m_BossState);
            //stream.SendNext(_BossC.HP);
            stream.SendNext(_BossC.r);

        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctBossPos = (Vector3)stream.ReceiveNext();
            correctBossRot = (Quaternion)stream.ReceiveNext();
            //_BossC.HP = (int)stream.ReceiveNext();
            _BossC.m_BossState = (BossState)(int)stream.ReceiveNext();
            _BossC.r = (int)stream.ReceiveNext();

            // avoids lerping the character from "center" to the "current" position when this client joins
            if (firstTake)
            {
                firstTake = false;
                this.transform.position = correctBossPos;
                transform.rotation = correctBossRot;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctBossPos, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctBossRot, Time.deltaTime);
        }
    }
}
