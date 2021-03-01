using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public enum BossState
    {
        Dead,

        Attack1,
        Attack2,
        Attack3,

        Move,
        Idle,
        Sleep,
        Hit,
        
    }
    public BossState _BossStatee;

    public GameObject ShotPos_r;
    public GameObject ShotPos_l;

    public GameObject player;
    
    EnemyValueControl _EnemyValueControl;
    public GameObject Boom;

    public Animator _Animator;
    public GameObject Buttle;


    public GameObject Gun;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;

    public GameObject Atk01;

    public AudioSource ATK1AudioSource;
    public AudioSource ATK2AudioSource;
    public AudioSource ATK3AudioSource;

    public bool StartATK=false;


    // Start is called before the first frame update
    void Start()
    {
        _Animator = this.GetComponent<Animator>();
        _EnemyValueControl = this.GetComponent<EnemyValueControl>();
        _BossStatee = BossState.Idle;
        _EnemyValueControl.SetValue(200, 200, 5);


        gunParticles = Gun.GetComponent<ParticleSystem>();
        gunLine = Gun.GetComponent<LineRenderer>();
        gunAudio = Gun.GetComponent<AudioSource>();
        gunLight = Gun.GetComponent<Light>();
        gunParticles.Stop();

    }




    // Update is called once per frame
    void Update()
    {
        if (_EnemyValueControl.NowHP() <= 0)
        {
            Dead();
        }
        if (_EnemyValueControl.NowHP() > 0)
        {
            SwitchState();


            
        }
        if (_Animator.GetBool("isIdle"))
        {
            _BossStatee = BossState.Idle;
        }
    }


    void SwitchState()
    {
        switch (_BossStatee)
        {

            case BossState.Idle:
                Idle();
                break;
            case BossState.Attack1:
                Attack1();
                break;
            case BossState.Dead:
                Dead();
                break;
            case BossState.Attack2:
                Attack2();
                break;
            case BossState.Attack3:
                Attack3();
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Sleep:
                Sleep();
                break;
            case BossState.Hit:
                Hit();
                break;

            default:

                break;
        }
    }

    void Behavior()
    {
        int i = RandomValue();
        if (i<2)
        {
            _BossStatee = BossState.Attack3;
        }
        else if (i < 4)
        {
            _BossStatee = BossState.Attack1;
        }else if (i < 6)
        {
            _BossStatee = BossState.Attack2;
        }
        else
        {
            _BossStatee = BossState.Idle;
            
        }
        
    }


    int RandomValue()
    {
        Random.seed = System.Guid.NewGuid().GetHashCode();
        int i = Random.Range(0, 10);
        //float f = Random.Range(0.0f, 10.0f);
        //float v = Random.value;
        
        return i;
    }

    void IdleEnd()
    {
        if(StartATK)
        Behavior();
    }

    public void Hit()
    {
        _BossStatee = BossState.Hit;

        if (!isATK)
        {
            _Animator.SetTrigger("Hit");
            isATK = true;
            _Animator.SetBool("isIdle", false);
            isUseReState = false;
        }
        else
        {
            _Animator.SetBool("ATK2", false);
        }
    }
    bool isUseReState = false;
    void Idle()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 5 * Time.deltaTime);

        if (!isUseReState)
        reState();
        
    }
    void Sleep()
    {

    }
    void Move()
    {

    }
 
    int count = 0;
    void Attack2()
    {
       
        if (!isATK)
        {
            ATK2AudioSource.Play();
            _Animator.SetBool("ATK2", true);
            isATK = true;
            _Animator.SetBool("isIdle", false);
            isUseReState = false;

          

            

        }
        else
        {
            
        }

    }

    void ATK2EndTime()
    {
       
        _Animator.SetBool("ATK2", false);
        _Animator.SetBool("ReturnIdle", true);
       
    }

    public GameObject Tager;
    public GameObject BulletGeneration;
    public GameObject Atk03;

    void MachineGun()
    {
        //ShotPos_r
        //ShotPos_l
        //Atk03

        ATK3AudioSource.Play();
         GameObject N = Instantiate(Atk03, ShotPos_r.transform.position, this.transform.rotation);
        N.GetComponent<Rigidbody>().AddForce(N.transform.forward);
        N.GetComponent<butt>().setPos(BulletGeneration, ShotPos_r);


    }

    void Attack3()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 5 * Time.deltaTime);
        if (!isATK)
        {
            BulletGeneration = Instantiate(Tager, player.transform.position, this.transform.rotation);
            _Animator.SetBool("ATK3", true);
            isATK = true;
            _Animator.SetBool("isIdle", false);
            isUseReState = false;
            




        }
        else
        {
            _Animator.SetBool("ATK3", false);
            _Animator.SetBool("ReturnIdle", true);
             
        }
    }

    void ATK3StartShot()
    {
        InvokeRepeating("MachineGun", 0.2f, 0.1f);
    }
    void ATK3EndShot()
    {
        CancelInvoke("MachineGun");
    }

    public void GunPost()
    {
        
        if (gunParticles.isStopped)
        {
            if (count == 0)
            {
               
                gunParticles.Play();
                count++;
            }
            else
            {
                // Invoke("RunAway", 2);
            }

        }
    }



    bool isATK=false;
    void Attack1()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 5 * Time.deltaTime);
        if (!isATK)
        {
            
            _Animator.SetBool("ATK1", true);
            isATK = true;
            _Animator.SetBool("isIdle", false);
            isUseReState = false;
        }
        else
        {
            _Animator.SetBool("ATK1", false);
            _Animator.SetBool("ReturnIdle", true);
        }



    }


    

    void ShotR()
    {
        ATK1AudioSource.Play();
        BulletGeneration = Instantiate(Tager, player.transform.position, this.transform.rotation);
        GameObject N = Instantiate(Atk01, ShotPos_r.transform.position, ShotPos_r.transform.rotation);
        N.GetComponent<Rigidbody>().AddForce(N.transform.forward);
        N.GetComponent<butt>().setPos(BulletGeneration, ShotPos_r);

    }
    void ShotL()
    {
        ATK1AudioSource.Play();
        BulletGeneration = Instantiate(Tager, player.transform.position, this.transform.rotation);
        GameObject N = Instantiate(Atk01, ShotPos_l.transform.position, ShotPos_l.transform.rotation);
        N.GetComponent<Rigidbody>().AddForce(N.transform.forward);
        N.GetComponent<butt>().setPos(BulletGeneration, ShotPos_l);
    }

    void reState()
    {
        isATK = false;
        count = 0;
        isUseReState = true;
        _Animator.SetBool("isIdle", true);
        _Animator.SetBool("ATK2", false);
        _Animator.SetBool("ATK1", false);
        _Animator.SetBool("ATK3", false);
        _Animator.SetBool("ReturnIdle", false);


    }

    bool IsBoom = true;
    public GameObject BoomP;
    public void Dead()
    {
        _Animator.SetBool("Life", false);
        if (IsBoom)
        {
            Instantiate(BoomP, this.transform.position+new Vector3(0,2,0), this.transform.rotation, this.gameObject.transform);
            IsBoom = false;
        }
        StartCoroutine("DelayTime", 2f);
    }
    public GameState _GameState;
    IEnumerator DelayTime(int time)
    {
        yield return new WaitForSeconds(time);
        _GameState.GameEnd();
        this.gameObject.SetActive(false);

    }

    public void CollisionDamage(Collider collider)
    {
        if (StartATK)
            DamageController.Damage(this.transform.gameObject, collider.gameObject, null);
       
    }
    public void GunDamage(GameObject attacker, GameObject collider)
    {
        if(StartATK)
        DamageController.Damage(attacker, collider, null);
    }

}
