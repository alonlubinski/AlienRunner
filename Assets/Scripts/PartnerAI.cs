using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PartnerAI : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform target1;
    private bool target1IsAlive = true;
    [SerializeField] Transform target2;
    private bool target2IsAlive = true;
    [SerializeField] Transform gunOnFloor;
    [SerializeField] GameObject gunInHand;
    [SerializeField] Transform grenadesOnFloor;
    NavMeshAgent navMeshAgent;
    private float distanceToTarget1 = Mathf.Infinity;
    private float distanceToTarget2 = Mathf.Infinity;
    private float distanceToGunOnFloor = Mathf.Infinity;
    private float distanceToGrenadesOnFloor = Mathf.Infinity;
    private bool isProvoked = false;
    private bool isAlive = true;
    private bool hasGun = false;
    private bool hasGrenades = false;
    private bool grenadeThrown = false;
    private bool gunShoot = false;
    public float delayGrenade = 3f;
    float countdownGrenade;
    public float delayGun = 1f;
    float countdownGun;
    public GameObject grenadePrefab;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] float range = 10f;
    [SerializeField] float size = 20f;
    [SerializeField] GameObject hitImpact;
    public AudioSource shotAudio;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        countdownGrenade = 0f;
        countdownGun = 0f;
        shotAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (countdownGrenade > 0f)
        {
            countdownGrenade -= Time.deltaTime;
            if (countdownGrenade <= 0f)
            {
                countdownGrenade = 0f;
                grenadeThrown = false;
                navMeshAgent.enabled = true;
            }
        }

        if (countdownGun > 0f)
        {
            countdownGun -= Time.deltaTime;
            if (countdownGun <= 0f)
            {
                countdownGun = 0f;
                gunShoot = false;
            }
        }


        if (isAlive && hasGun && hasGrenades)
        {
            if (navMeshAgent.enabled)
            {
                navMeshAgent.SetDestination(player.position);
                distanceToTarget1 = Vector3.Distance(target1.position, transform.position);
                target1IsAlive = target1.GetComponent<AlienAI>().IsAlive();
                distanceToTarget2 = Vector3.Distance(target2.position, transform.position);
                target2IsAlive = target2.GetComponent<AlienAI>().IsAlive();
                if (distanceToTarget1 > 10f && distanceToTarget2 > 10f && isAlive && target1IsAlive && target2IsAlive)
                {
                    isProvoked = false;
                    Debug.Log("Searching...");
                } 
                else if (isProvoked && isAlive)
                {
                    // Target close enough to attack
                    if(target1IsAlive && target2IsAlive)
                    {
                        if(distanceToTarget1 < distanceToTarget2)
                        {
                            AttackTarget(target1);
                        } else
                        {
                            AttackTarget(target2);
                        }
                    } else if(target1IsAlive && !target2IsAlive)
                    {
                        AttackTarget(target1);
                    } else if(!target1IsAlive && target2IsAlive)
                    {
                        AttackTarget(target2);
                    }
                    
                }
                else if ((distanceToTarget1 <= 10f || distanceToTarget2 <= 10f) && isAlive)
                {
                    isProvoked = true;
                }
            }

        }
        else if (isAlive && !hasGun && !hasGrenades)
        {
            navMeshAgent.SetDestination(gunOnFloor.position);
            distanceToGunOnFloor = Vector3.Distance(gunOnFloor.position, transform.position);
            if (distanceToGunOnFloor < 3.5f)
            {
                PickGun();
            }
        }
        else if (isAlive && hasGun && !hasGrenades)
        {
            navMeshAgent.SetDestination(grenadesOnFloor.position);
            distanceToGrenadesOnFloor = Vector3.Distance(grenadesOnFloor.position, transform.position);
            if (distanceToGrenadesOnFloor < 2.9f)
            {
                PickGrenades();
            }
        }
    }


    private void AttackTarget(Transform target)
    {
        Debug.Log("Attack!!");
        navMeshAgent.SetDestination(target.position);
        NavMeshHit hit;
        if (!navMeshAgent.Raycast(target.position, out hit))
        {
            // Player is visible to hit
            Debug.Log("Shoot bullet!");
            if (!gunShoot)
            {
                gunShoot = true;
                muzzle.Play();
                shotAudio.Play();
                // Shoot with gun
                ShootBullet();
                countdownGun = delayGun;
            }
        }
        else
        {
            Debug.Log("Throw Grenade!");
            if (!grenadeThrown)
            {
                grenadeThrown = true;
                // Throw grenade
                ThrowGrenade();
                countdownGrenade = delayGrenade;
                navMeshAgent.enabled = false;
            }
        }
    }

    private void PickGun()
    {
        gunOnFloor.gameObject.SetActive(false);
        gunInHand.SetActive(true);
        hasGun = true;
    }

    private void PickGrenades()
    {
        grenadesOnFloor.gameObject.SetActive(false);
        hasGrenades = true;
    }

    private void ShootBullet()
    {
        muzzle.Play();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range * 20))
        {
            CreateHitImpact(hit);
            if (hit.transform.CompareTag("Player"))
            {
                PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
                player.TakeDamage(size);
            } else if (hit.transform.CompareTag("Alien"))
            {
                EnemyHealth alien = hit.transform.GetComponent<EnemyHealth>();
                alien.TakeDamage(size);
            }


        }
        else
        {
            return;
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(transform.forward * 10, ForceMode.Impulse);
    }

    public void Kill()
    {
        isAlive = false;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitImpact, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .5f);
    }
}
