using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManage : MonoBehaviour
{



    public Animator _animator;

    public enum GunKind
    {
        BasicGun,
        LaserGun,
        ShieldGun,

    }

    public int hand;
    public int nowhand;

    public BasicGun _BasicGun;
    public LaserGun _LaserGun;
    public ShieldGun _ShieldGun;
    int DamageValue;
    public GunKind _gunKind;


    public GameObject GunModle;

    // Start is called before the first frame update
    void Start()
    {
        _BasicGun= gameObject.GetComponent<BasicGun>();
        _LaserGun = gameObject.GetComponent<LaserGun>();
        _ShieldGun = gameObject.GetComponent<ShieldGun>();



        _animator = GunModle.GetComponent<Animator>();
        _gunKind = GunKind.BasicGun;
    }

    // Update is called once per frame
    void Update()
    {
        if (VRInput.GetAxis(VRInput.Input.LeftAxis).x != 0 || VRInput.GetAxis(VRInput.Input.LeftAxis).y != 0)
        {


            nowhand = 1;
            DiscSwitching(VRInput.GetAxis(VRInput.Input.LeftAxis).x, VRInput.GetAxis(VRInput.Input.LeftAxis).y, 1);

            switchEnum(1);
        }
        if (VRInput.GetAxis(VRInput.Input.RightAxis).x != 0 || VRInput.GetAxis(VRInput.Input.RightAxis).y != 0)
        {

            nowhand = 0;
            DiscSwitching(VRInput.GetAxis(VRInput.Input.RightAxis).x, VRInput.GetAxis(VRInput.Input.RightAxis).y, 0);
            switchEnum(0);
        }
        

  

    }

    void switchEnum(int nh)
    {   
        if(hand== nh)
        switch (_gunKind)
        {
            case GunKind.BasicGun:
                BasicGun();
                break;
            case GunKind.LaserGun:
                LaserGun();
                break;
            case GunKind.ShieldGun:
                ShieldGun();
                break;
            default:
                break;
        }
    }

    void BasicGun()
    {
        if (!_BasicGun.enabled)
        {

            _BasicGun.enabled = true;
            _LaserGun.enabled = false;
            _ShieldGun.ShieldSetActive();
            _ShieldGun.enabled = false;
            
        }
        
        DamageValue = _BasicGun.DamageValue;
        //^=反轉
    }
    void LaserGun()
    {
        if (!_LaserGun.enabled)
        {
            _ShieldGun.ShieldSetActive();
            _BasicGun.enabled = false;           
            _ShieldGun.enabled = false;
            _LaserGun.enabled = true;

        }
        DamageValue = _LaserGun.DamageValue;
    }

    void ShieldGun()
    {
        if (!_ShieldGun.enabled)
        {
            _BasicGun.enabled = false;       
            _LaserGun.enabled = false;
            _ShieldGun.enabled = true;
        }
    }
    

    public int damagneInt()
    {
        if (_LaserGun.enabled == true)
        {
            return _LaserGun.GetDamage();
        }
        if (_BasicGun.enabled == true)
        {
            return _BasicGun.GetDamage();
            
        }
      /*  if (_ShieldGun.enabled == true)
        {
            return _ShieldGun.GetDamage();

        }*/
        return 0;
    }


    void DiscSwitching(float x,float y,int z)
    {

        if(z==hand)
        if (y > 0)
        {
            if (x > 0)
            {
                if (x < y)
                {
                    // print("第一象限");
                    _animator.SetBool("SB", true);
                    _animator.SetBool("LB", true);
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guns1Idle"))
                    {
                        reSetAnimator();
                        
                        _gunKind = GunKind.LaserGun;
                    }
                }
                else
                {
                    reSetAnimator();
                    //   print("第四象限");
                }
            }
            else
            {
                if (x * -1 < y)
                {
                    // print("第一象限");
                    _animator.SetBool("SB", true);
                    _animator.SetBool("LB", true);
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guns1Idle"))
                    {
                        reSetAnimator();
                        _gunKind = GunKind.LaserGun;
                    }
                   
                }
                else
                {
                    //   print("第二象限");
                    _animator.SetBool("SL", true);
                    _animator.SetBool("BL", true);
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guns2Idle"))
                    {
                        reSetAnimator();
                        _gunKind = GunKind.BasicGun;
                        
                    }
                    
                }
            }
        }
        else
        {
            if (x > 0)
            {
                if (x < y * -1)
                {
                    _animator.SetBool("LS", true);
                    _animator.SetBool("BS", true);
                    //  print("第三象限");
                    
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guns3Idle"))
                    {
                        reSetAnimator();
                        _gunKind = GunKind.ShieldGun;
                    }
                }
                else
                {
                    reSetAnimator();
                    //  print("第四象限");
                }
            }
            else
            {
                if (x * -1 < y*-1)
                {
                    //  print("第三象限");
                    _animator.SetBool("LS", true);
                    _animator.SetBool("BS", true);
                    
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guns3Idle"))
                    {
                        reSetAnimator();
                        _gunKind = GunKind.ShieldGun;
                    }
                }
                else {
                    //  print("第二象限");
                    _animator.SetBool("SL", true);
                    _animator.SetBool("BL", true);
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guns2Idle"))
                    {
                        reSetAnimator();
                        _gunKind = GunKind.BasicGun;
                        
                    }
                   
                }
            }
        }

    }

    public void ShotAnimator(int nh)
    {
        if (nh == hand)
        {
            _animator.SetTrigger("BShot");
            _animator.SetTrigger("LShot");
            _animator.SetTrigger("SShot");
        }
     
    }


    void reSetAnimator()
    {
        _animator.SetBool("SL", false);
        _animator.SetBool("BL", false);
        _animator.SetBool("LS", false);
        _animator.SetBool("LB", false);
        _animator.SetBool("SB", false);
        _animator.SetBool("BS", false);
       
    }






}
