using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum BossState
{
    Idle = 0,
    Attack1 = 1,
    Attack2 = 2,
    Attack3 = 3,
    Attack4 = 4,
    Call = 5,
    Death = 6
}

public class BossC : Photon.MonoBehaviour
{
    public GameObject BossModle;
    public BossState m_BossState = BossState.Idle;
    public Animator m_animator;
    public Transform m_PlayerTransform;
    public Quaternion e_Rotation;
    public int r;
    public GameObject CallPos;
    public bool AniB = true;
    public GameObject Enemy;
    public GameObject EnemyPos;
    AnimatorStateInfo info;
    public int HP = 40;
    public int EnemyInt = 0;
    public BossMove _BossMove;
    private void Awake()
    {
        HP = 40;
        Random.seed = System.Guid.NewGuid().GetHashCode();
        Rre();

    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject);
    }

    private void FixedUpdate()
    {

        
        BossStateGO(r);
        info = m_animator.GetCurrentAnimatorStateInfo(0);


    }

    IEnumerator Delay(float d)
    {
        yield return new WaitForSeconds(d);
        
    }



    void Call()
    {
        //PhotonNetwork.Instantiate(this.Enemy.name, CallPos.transform.position, Quaternion.identity, 0);
        if (AniB)
        {
            EnemyInt = 0;
            m_animator.Play("mini");
            
            StartCoroutine("Delay",3.0f);

            AniB = false;
        }
       
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            InvokeRepeating("ADD", 0, 1.0f);

        }
        else
        {




        }

        if (info.normalizedTime >= 1.0f && info.IsName("mini"))
        {
            AniB = true;
            Rre();
        }
    }

    void Attack1()
    {
        if (AniB)
        {
            AniB = false;
            _BossMove.Xspeed = true;
            m_animator.Play("L_atk1");
        }
        if (info.normalizedTime >= 1.0f && info.IsName("L_atk1"))
        {
            _BossMove.Xspeed = false;
            AniB = true;
            r = 8;
        }
    }
    void Attack2()
    {
        if (AniB)
        {
            AniB = false;
            m_animator.Play("L_atk2");
            _BossMove.Xspeed = true;
        }
        if (info.normalizedTime >= 1.0f && info.IsName("L_atk2"))
        {
            AniB = true;
            _BossMove.Xspeed = false;
            r = 8;
        }
    }
    void Attack3()
    {
        if (AniB)
        {
            AniB = false;
            m_animator.Play("R_atk1");
            _BossMove.Xspeed = true;
        }
        if (info.normalizedTime >= 1.0f && info.IsName("R_atk1"))
        {
            _BossMove.Xspeed = false;
            AniB = true;
            r = 8;

        }
    }
    void Attack4()
    {
        if (AniB)
        {
            AniB = false;
            m_animator.Play("R_atk2");
            _BossMove.Xspeed = true;
        }
        if (info.normalizedTime >= 1.0f && info.IsName("R_atk2"))
        {
            AniB = true;
            _BossMove.Xspeed = false;
            r = 8;
        }
    }

    void Death()
    {
       
            AniB = false;
            m_animator.Play("die");
            _BossMove.Xspeed = true;
            StartCoroutine("DieDelay", 2.0f);
            
    }
    IEnumerator DieDelay(float d)
    {
        yield return new WaitForSeconds(d);
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("BossDeath", PhotonTargets.All);

    }
    [PunRPC]
    void BossDeath()
    {

        BossModle.SetActive(false);
        StartCoroutine("GoMapDelay");

    }
    IEnumerator GoMapDelay()
    {
        yield return new WaitForSeconds(3.0f);
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ReMap", PhotonTargets.All);

    }

    [PunRPC]
    void ReMap()
    {
        Application.LoadLevel("DemoWorker-Scene");
    }



    void Idle()
    {
        if (AniB)
        {
            AniB = false;
            m_animator.Play("idle");
            StartCoroutine("Delay", 2.0f);
        }
        if (info.normalizedTime >= 1.0f && info.IsName("idle"))
        {
            AniB = true;
            Rre();
        }
    }

    [PunRPC]
    void TPR(int rr)
    {
        r = rr;
        print(r);
    }

    void Rre()
    {
        //if()

        if(photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            r = Random.Range(0, 10);
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("TPR", PhotonTargets.All,r);
        }
        else
        {
            



        }
       
    }





    void BossStateGO(int r)
    {

        if (HP <= 0)
        {
            m_BossState = BossState.Death;
        }
        else
        {
            
            if (r == 1)
            {
                m_BossState = BossState.Attack1;
            }
            else if (r == 2)
            {
                m_BossState = BossState.Attack2;
            }
            else if (r == 3)
            {
                m_BossState = BossState.Attack3;
            }
            else if (r == 4)
            {
                m_BossState = BossState.Attack4;
            }
            else if (r == 5)
            {
                m_BossState = BossState.Call;
            }
            else if (r == 6)
            {
                m_BossState = BossState.Call;
            }
            else if (r == 7)
            {
                m_BossState = BossState.Call;
            }
            else
            {
                m_BossState = BossState.Idle;
            }

        }

        updateState();

    }


    void updateState()
    {
        switch (m_BossState)
        {
            case BossState.Call:
                Call();

                break;
            case BossState.Attack1:
                Attack1();
                break;
            case BossState.Attack2:
                Attack2();
                break;
            case BossState.Attack3:
                Attack3();
                break;
            case BossState.Attack4:
                Attack4();
                break;

            case BossState.Death:
                Death();
                break;
            default:
                Idle();
                break;


        }
    }

    [PunRPC]
    void DamageBoss()
    {
        HP = HP - 1;
        print("111" + " " + HP);
    }

    void ADD()
    {
        if (EnemyInt <= 3)
        {
            PhotonNetwork.Instantiate(this.Enemy.name, EnemyPos.transform.position, Quaternion.identity, 0);
            StartCoroutine("Delay", 0.5f);
            EnemyInt++;
        }
    }

   



}
