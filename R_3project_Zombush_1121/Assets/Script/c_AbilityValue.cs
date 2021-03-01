using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_AbilityValue : AbilityValue
{
    
    // Use this for initialization
    void Start () {
        CharacterSet(1000, 1000, 100, 100, 1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("BoomPlay", PhotonTargets.All);


        }
 

    }
    [PunRPC]
    void CarDamage(int d)
    {
        HP = HP - d;
    }



}
