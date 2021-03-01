using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_UI : MonoBehaviour {
    public float MaxBullet;
    public float NowBullet;
    public GunS _Guns;
    // Use this for initialization
    void Start () {
		
	}
    private void Awake()
    {
        MaxBullet = _Guns.magazineMaxSize;
    }

    // Update is called once per frame
    void Update () {
        NowBullet = _Guns.magazineSize;
        this.transform.localPosition = new Vector3(0, -100 + 100 * (NowBullet / MaxBullet), 0);
    }
}
