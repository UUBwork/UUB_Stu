using UnityEngine;

public class ShieldGun : MonoBehaviour
{
    public int ShieldHP = 0;
    private int ShieldMaxHP = 99999;

    public Transform muzzleTransform;

    public float RELoad;

    public int hand; //左1右0

    public int DamageValue = 0;
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

    public GunState _GunState = new GunState();

    GunManage _GunManage;

    public GameObject Aircraft;


    float timer;


    public GameObject Shield;




    public enum GunState
    {

        Openshield,  //開頓
        

    }

    // Use this for initialization
    void Start()
    {
        // laserLine = GetComponent<LineRenderer>();


        _GunManage = this.GetComponent<GunManage>();
        ShieldMaxHP = 20;
        ShieldHP = ShieldMaxHP;
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = Gun.GetComponent<ParticleSystem>();
        gunLine = Gun.GetComponent<LineRenderer>();
        gunAudio = Gun.GetComponent<AudioSource>();
        gunLight = Gun.GetComponent<Light>();

        _GunState = GunState.Openshield;

    }

    // Update is called once per frame
    void Update()
    {
        //  print(Aircraft.transform.localRotation.eulerAngles.y);


     
        Openshield();


        timer += Time.deltaTime;




       

        /* if (VRInput.GetButtonDown(VRInput.Input.LeftTrigger))
         {
             magazineSize = magazineMaxSize;


         }*/

    }
   




    

   
    public GameObject bullets;
    void Openshield()
    {
        _GunState = GunState.Openshield;
        if (VRInput.GetButtonDown(VRInput.Input.RightTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 0 && ShieldHP > 0)
        {
            
            Shield.SetActive(true);
            
            CancelInvoke("RecoveryShieldHP");

        }
        else if (VRInput.GetButtonDown(VRInput.Input.LeftTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 1 && ShieldHP > 0)
        {

            CancelInvoke("RecoveryShieldHP");
            Shield.SetActive(true);
            
        }


        if (VRInput.GetButtonUp(VRInput.Input.RightTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 0 && DamageValue > 0)
        {

            Shield.SetActive(false);
        }

        else if (VRInput.GetButtonUp(VRInput.Input.LeftTrigger) && timer >= timeBetweenBullets && Time.timeScale != 0 && hand == 1 && DamageValue > 0)
        {

            Shield.SetActive(false);

        }
        
    }
    
    public void RecoveryShieldHP()
    {
        if(ShieldHP< ShieldMaxHP)
        ShieldHP = ShieldHP + 10;
    }



    public void AccumulationDamage(int e_damage)
    {
        _GunManage.ShotAnimator(hand);
        DamageValue = DamageValue + e_damage;
        ShieldHP = ShieldHP - e_damage;

    }

    public void ShieldSetActive()
    {
       Shield.SetActive(false);
    }


    public int GetDamage()
    {

        return DamageValue;
    }
}
