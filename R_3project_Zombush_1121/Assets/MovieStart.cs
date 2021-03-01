using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieStart : Photon.MonoBehaviour
{
    public BossC BossC;
    public BossMove BossMove;
    public Animation BossAnimation;
    public CarC _CarC;
    public GameObject zobwalll;
    public AudioSource _AudioSource;
    public AudioSource _BgmAudioSource;
    // Use this for initialization
    void Start () {

        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("CloseBoss", PhotonTargets.All);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _CarC.enabled = false;
            
            print("序幕");
            
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("BossAni", PhotonTargets.All);
        
            StartCoroutine("StartDelay");
        }


    }


    [PunRPC]
    void CloseBoss()
    {
        BossC.enabled = false;
        BossMove.enabled = false;
        
        print("關閉boss");
       
    }
    [PunRPC]
    void BossAni()
    {
        _AudioSource.enabled = true;
        _BgmAudioSource.enabled = false;
        BossAnimation.Play();

        print("開啟boss");

    }


    [PunRPC]
    void OpenBoss()
    {
        
        BossC.enabled = true;
        BossMove.enabled = true;
        _CarC.enabled = true;
        print("開啟boss");

    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2.0f);
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("OpenBoss", PhotonTargets.All);
        zobwalll.active = true;
        Destroy(this.gameObject);
    }



}
