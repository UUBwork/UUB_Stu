using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZobWallNet : Photon.MonoBehaviour
{

    private Vector3 correctEnemyPos = Vector3.zero; //We lerp towards this
    private Quaternion correctEnemyRot = Quaternion.identity; //We lerp towards this
    public ZobWall _ZobWall;
    public WallMove _wallMove;
    void OnEnable()
    {
     
    }

    void Awake()
    {

        _ZobWall = GetComponent<ZobWall>();
        _wallMove = GetComponent<WallMove>();
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            _ZobWall.enabled = true;
            _wallMove.enabled = true;
        }
        else
        {

            _ZobWall.enabled = false;
            _wallMove.enabled = false;

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
            correctEnemyPos = (Vector3)stream.ReceiveNext();
            correctEnemyRot = (Quaternion)stream.ReceiveNext();


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctEnemyPos, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctEnemyRot, Time.deltaTime);
        }
    }
}
