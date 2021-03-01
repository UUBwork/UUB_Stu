using UnityEngine;

public class PlayerShooting2 : MonoBehaviour
{
   // public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public Player2Input _Player2Input;
    public GunS _GunS;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit hit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    public GameObject OP;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Default");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

        //if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        if (_Player2Input.Fire && timer >= timeBetweenBullets && Time.timeScale != 0 && _GunS.magazineSize>0)
        {
           // Shoot ();
            PhotonView e_photonView = PhotonView.Get(this);
            e_photonView.RPC("Shoot", PhotonTargets.All);
            _GunS.magazineSize--;
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
        if (_Player2Input.reFire)
        {
            _GunS.magazineSize = _GunS.magazineMaxSize;


        }

    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    [PunRPC]
    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, OP.transform.position);

        shootRay.origin = OP.transform.position;
        shootRay.direction = OP.transform.forward;

        if(Physics.Raycast (shootRay, out hit, range, shootableMask))
        {
            //EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            /*if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }*/

            print(hit.collider.tag);

            if (hit.collider.tag == "enemy")
            {

                PhotonView e_photonView = PhotonView.Get(hit.collider.GetComponent<ec>());
                e_photonView.RPC("DamageEnemy", PhotonTargets.All);
                print("打到敵人");
                

            }
            if (hit.collider.tag == "Boss")
            {

                PhotonView e_photonView = PhotonView.Get(hit.collider.GetComponent<BossC>());
                e_photonView.RPC("DamageBoss", PhotonTargets.All);
                print("打到boss");


            }


            gunLine.SetPosition (1, hit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
