using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{

    public int magazineSize = 0;
    private int magazineMaxSize = 50;

    public Transform muzzleTransform;

    public float RELoad;

    public int hand; //左1右0

    public int DamageValue = 5;
    public int  OrangeDamage = 1;

    public float timeBetweenBullets = 0.15f;
    public float range = 3500f;
    float effectsDisplayTime = 0.2f;

    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;

    public GameObject Gun;

    public GunState _GunState = new GunState();
    public AudioSource reLoad;

    public int GatherValue = 0;

    public GameObject Aircraft;

   

    float timer;
    GunManage _gunManage;




    public enum GunState
    {
        SingleShot,  //單發
        ContinuousShooting,//連射
        SwitchShot,  //切換子彈
        UnableShoot, //無法射擊
      //  GatherShot, //聚能
    }

    // Use this for initialization
    void Start()
    {
        // laserLine = GetComponent<LineRenderer>();

        _gunManage = this.GetComponent<GunManage>();

        magazineMaxSize = 50;
        magazineSize = magazineMaxSize;
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = Gun.GetComponent<ParticleSystem>();
        gunLine = Gun.GetComponent<LineRenderer>();
        gunAudio = Gun.GetComponent<AudioSource>();
        gunLight = Gun.GetComponent<Light>();
        reLoad = this.GetComponent<AudioSource>();
        _GunState = GunState.SingleShot;

    }

    // Update is called once per frame
    void Update()
    {

       
        Debug.DrawRay(muzzleTransform.transform.position, muzzleTransform.transform.forward * 3500, Color.green);
        PlayerAction();
        SwitchState();


        timer += Time.deltaTime;




        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
           // DisableEffects();
        }

     
    }



    void PlayerAction()
    {

        if (gameObject.transform.localRotation.eulerAngles.x > 70 && gameObject.transform.localRotation.eulerAngles.x < 120)
        {
            reLoad.Play();
            //_GunState = GunState.SwitchShot;
            SwitchShot();
        }
        /*
        if (gameObject.transform.localRotation.eulerAngles.x > 240 && gameObject.transform.localRotation.eulerAngles.x < 290)
        {
            //_GunState = GunState.SwitchShot;
            
            InvokeRepeating("FillGun", 0, 0.1f);
            // print("聚集中");
            
        }
        else
        {
            CancelInvoke("FillGun");
            DamageValue = GatherValue + DamageValue;
            GatherValue = 0;
        }
        */
    }


    void SwitchState()
    {
        switch (_GunState)
        {

            case GunState.SingleShot:
                SingleShot();
                break;
            case GunState.ContinuousShooting:
                ContinuousShooting();
                break;
            case GunState.SwitchShot:
                SwitchShot();
                break;

            case GunState.UnableShoot:
                
                break;
            
            default:

                break;
        }
    }
    float GatherTime = 0;
    bool FreeBullet = false;
    void FillGun()
    {
        if (magazineSize > 0)
        {

            magazineSize = (magazineSize - 1);
            GatherValue = GatherValue+1;
            FreeBullet = true;
        }
    }


    void SwitchShot()
    {
        //播放聲音 未完成
        //時間到才算切換成功
        _GunState = GunState.SwitchShot;
        timer += Time.deltaTime;
        if (timer >= RELoad)
        {



            timer = 0;

            magazineSize = magazineMaxSize;
            //在射擊的時候生成槍火的動畫

            _GunState = GunState.SingleShot;
        }


    }

    void SingleShot()
    {
        GatherTime = 0;
        _GunState = GunState.SingleShot;
        if (VRInput.GetButtonDown(VRInput.Input.RightTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 0 && (magazineSize > 0 | FreeBullet))
        {
            magazineSize--;
            Shoot();
            DamageValue = GatherValue + DamageValue;
            DamageValue = OrangeDamage;
            GatherValue = 0;

        }
        else if (VRInput.GetButtonDown(VRInput.Input.LeftTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 1 && (magazineSize > 0 | FreeBullet))
        {
            magazineSize--;
            Shoot();
            DamageValue = GatherValue + DamageValue;
            DamageValue = OrangeDamage;
            GatherValue = 0;
        }
    }

    void ContinuousShooting()
    {
        _GunState = GunState.ContinuousShooting;
        if (VRInput.GetButtonUpdate(VRInput.Input.RightTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 0 && magazineSize > 0)
        {
            magazineSize--;
            Shoot();


        }
        else if (VRInput.GetButtonUpdate(VRInput.Input.LeftTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 1 && magazineSize > 0)
        {
            magazineSize--;
            Shoot();

        }
    }
    
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot()
    {
       
        timer = 0f;

        //gunAudio.Play();

        //gunLight.enabled = true;
        gunAudio.Play();
        gunParticles.Stop();
        gunParticles.Play();

       // gunLine.enabled = true;
       // gunLine.SetPosition(0, muzzleTransform.transform.position);
        
        shootRay.origin = muzzleTransform.transform.position;
        shootRay.direction = muzzleTransform.transform.forward;
        _gunManage.ShotAnimator(hand);

        if (Physics.Raycast(shootRay, out shootHit, range, 1 << 0))
        {


          
                DamageController.Damage(this.gameObject, shootHit.collider.gameObject, null);
                
            
          //  gunLine.SetPosition(30, shootHit.point);
           // print(shootHit.collider.name);
        }
        else
        {
            //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }


    public int GetDamage()
    {
        return DamageValue;
    }



 




}





