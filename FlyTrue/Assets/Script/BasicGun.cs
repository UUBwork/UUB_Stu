using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{

    public int magazineSize = 0;
    private int magazineMaxSize = 20;

    public Transform muzzleTransform;
    
    public float RELoad;

    public int hand; //左1右0

    public int DamageValue = 2;
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 350f;
    float effectsDisplayTime = 0.2f;

    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;

    public GameObject Gun;

    public AudioSource reLoad;

    public GunState _GunState = new GunState();


    public GameObject Aircraft;


    float timer;


    GunManage _gunManage;


    public enum GunState
    {
        SingleShot,  //單發
        ContinuousShooting,//連射
        SwitchShot,  //切換子彈
        UnableShoot, //無法射擊
        SpecialShot, //特殊射擊

    }

    // Use this for initialization
    void Start()
    {
        // laserLine = GetComponent<LineRenderer>();

        _gunManage = this.GetComponent<GunManage>();
        reLoad=this.GetComponent<AudioSource>();
        magazineMaxSize = 20;
        magazineSize = magazineMaxSize;
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = Gun.GetComponent<ParticleSystem>();
        gunLine = Gun.GetComponent<LineRenderer>();
        gunAudio = Gun.GetComponent<AudioSource>();
        gunLight = Gun.GetComponent<Light>();

        _GunState = GunState.SingleShot;

    }

    // Update is called once per frame
    void Update()
    {
      //  print(Aircraft.transform.localRotation.eulerAngles.y);


        PlayerAction();
        SwitchState();
        

        timer += Time.deltaTime;




        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        /* if (VRInput.GetButtonDown(VRInput.Input.LeftTrigger))
         {
             magazineSize = magazineMaxSize;


         }*/

    }



    void PlayerAction()
    {

        if (gameObject.transform.localRotation.eulerAngles.x > 70 && gameObject.transform.localRotation.eulerAngles.x < 120)
        {
            //_GunState = GunState.SwitchShot;
            reLoad.Play();
            SwitchShot();
            
        }
        if ((gameObject.transform.localRotation.eulerAngles.y ) %360 < 270+ Aircraft.transform.localRotation.eulerAngles.y % 360 - 270 && (gameObject.transform.localRotation.eulerAngles.y ) % 360 > 240 + Aircraft.transform.localRotation.eulerAngles.y % 360 - 270 && hand==1) //左手數值
        {
            //_GunState = GunState.ContinuousShooting;
            
            ContinuousShooting();
        }
        if ((gameObject.transform.localRotation.eulerAngles.y ) % 360 > 90 + Aircraft.transform.localRotation.eulerAngles.y % 360 - 270 && (gameObject.transform.localRotation.eulerAngles.y ) % 360 < 140 + Aircraft.transform.localRotation.eulerAngles.y % 360 - 270 && hand == 0) //右手數值
        {
            ContinuousShooting();
            //_GunState = GunState.ContinuousShooting;
        }

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
            case GunState.SpecialShot:

                break;
            default:

                break;
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
        _GunState = GunState.SingleShot;
        if (VRInput.GetButtonDown(VRInput.Input.RightTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 0 && magazineSize > 0)
        {
            magazineSize--;
            Shoot();


        }
        else if (VRInput.GetButtonDown(VRInput.Input.LeftTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 1 && magazineSize > 0)
        {
            magazineSize--;
            Shoot();

        }
    }

    void ContinuousShooting()
    {
        _GunState = GunState.ContinuousShooting;
        if (VRInput.GetButtonUpdate(VRInput.Input.RightTrigger) && timer >= timeBetweenBullets-0.3F && Time.timeScale != 0 && hand == 0 && magazineSize > 0)
        {
            magazineSize--;
            Shoot();


        }
        else if (VRInput.GetButtonUpdate(VRInput.Input.LeftTrigger) && timer >= timeBetweenBullets - 0.3F && Time.timeScale != 0 && hand == 1 && magazineSize > 0)
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

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, muzzleTransform.transform.position);

        shootRay.origin = muzzleTransform.transform.position;
        shootRay.direction = muzzleTransform.transform.forward;
        _gunManage.ShotAnimator(hand);
        if (Physics.Raycast(shootRay, out shootHit, range,1<<0))
        {
            
            DamageController.Damage(this.gameObject, shootHit.collider.gameObject, null);
            print(shootHit.collider.name);
            gunLine.SetPosition(1, shootHit.point);

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }




    public int GetDamage()
    {
        return DamageValue;
    }




}





