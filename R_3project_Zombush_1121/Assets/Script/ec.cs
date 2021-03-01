using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    Idle=0,
    Run=1,
    Death=2,
    Boom=3
}

public class ec : Photon.MonoBehaviour
 {


    public State m_state = State.Idle;
    public float SeeRange = 1000.0f;
    public float runSpeed = 10.0f;

    public Animator m_animator;
    //public Rigidbody m_rigidbody;

    public e_AbilityValue e_AbilityValue;
    public Transform m_PlayerTransform;

    public float diatanceToPlayer;
    public Quaternion e_Rotation;
    //public GameObject player;

    public int BoomATK =1;
    public bool BoomBool=false;

    public int HP = 5;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SeeRange);
    }

    // Use this for initialization
    void Start () {
        
    }
    private void Awake()
    {
        PhotonView photonView = PhotonView.Get(this);
        
        e_AbilityValue = GetComponent<e_AbilityValue>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, SeeRange, 1 << 8);
        if (playerColliders.Length > 0)
        {
            if (m_PlayerTransform == null)
            {
                if (Vector3.Distance(transform.position, playerColliders[0].transform.position) <= Vector3.Distance(transform.position, playerColliders[1].transform.position))
                {
                    m_PlayerTransform = playerColliders[0].transform;
                    diatanceToPlayer = Vector3.Distance(m_PlayerTransform.position, transform.position);
                }
                else
                {
                    m_PlayerTransform = playerColliders[1].transform;
                    diatanceToPlayer = Vector3.Distance(m_PlayerTransform.position, transform.position);
                }
            }
            

        }

    
            if (!BoomBool)
            {
                if (HP > 0)
                {

                    if (playerColliders.Length > 0)
                    {

                        m_state = State.Run;
                    }

                }

                if (HP <= 0)
                {
                    m_state = State.Death;
                }
            }


        
            switch (m_state)
        {
            case State.Run:
                Run();
               // photonView.RPC("Run", PhotonTargets.All);
                transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                e_Rotation = Quaternion.LookRotation(m_PlayerTransform.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, e_Rotation, 1.0f);
                break;
            case State.Death:
                Death();
                //photonView.RPC("Death", PhotonTargets.All);
                break;
            
            default:
                Idle();
                //photonView.RPC("Idle", PhotonTargets.All);
                break;


        }
    }
    [PunRPC]
    void Run()
    {
        diatanceToPlayer = Vector3.Distance(m_PlayerTransform.transform.position, transform.position);
        m_animator.Play("Running");
    }
    [PunRPC]
    void Death()
    {
        m_animator.Play("Death");
        
        
        StartCoroutine("DeathDelay");
       

    }
    [PunRPC]
    void Idle()
    {

        m_animator.Play("Idle");
    }

  

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    
    void OnCollisionEnter(Collision other)
    {
       
        if (other.collider.tag == "Player")
        {
            BoomBool = true;
            m_state = State.Death;
            print("Player");
            other.collider.GetComponent<c_AbilityValue>().HP = other.collider.GetComponent<c_AbilityValue>().HP - BoomATK;
        }

        if (other.collider.tag == "ZobWall")
        {

            StartCoroutine("DeathDelay");

        }

        if (other.collider.tag == "Wall")
        {

            m_state = State.Death;

        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BoomBool = true;
            m_state = State.Death;
            print("Player");
            other.GetComponent<c_AbilityValue>().HP = other.GetComponent<c_AbilityValue>().HP - BoomATK;

        }

        if (other.tag == "ZobWall")
        {

            StartCoroutine("DeathDelay");

        }
        if (other.tag == "Wall")
        {
            m_state = State.Death;
        }
    }



    [PunRPC]
    void DamageEnemy()
    {
        HP = HP - 1;
        print("111"+" "+ HP);
    }


}
