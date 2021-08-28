using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 10f;
    [SerializeField] float size = 20f;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] GameObject hitImpact;
    public AudioSource shotAudio;

    // Start is called before the first frame update
    void Start()
    {
        shotAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        muzzle.Play();
        shotAudio.Play();
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range * 20))
        {
            CreateHitImpact(hit);
            if (hit.transform.name.Equals("Body"))
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                target.TakeDamage(size);
            } else if (hit.transform.CompareTag("Partner"))
            {
                PartnerHealth partner = hit.transform.GetComponent<PartnerHealth>();
                partner.TakeDamage(size);
            }

            
        } else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitImpact, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .5f);
    }
}
