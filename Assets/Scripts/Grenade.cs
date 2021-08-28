using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float size = 10f;
    public float delay = 3f;
    float countdown;
    public float radius = 5f;
    public float force = 700f;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionSound.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, 10.0f);
            }
            if (nearbyObject.CompareTag("Alien"))
            {
                EnemyHealth target = nearbyObject.GetComponent<EnemyHealth>();
                target.TakeDamage(size);
            } else if (nearbyObject.CompareTag("Player"))
            {
                PlayerHealth player = nearbyObject.GetComponent<PlayerHealth>();
                player.TakeDamage(size);
            } else if (nearbyObject.CompareTag("Partner"))
            {
                PartnerHealth partner = nearbyObject.GetComponent<PartnerHealth>();
                partner.TakeDamage(size);
            }
        }
        while(!explosionSound.isPlaying)
            Destroy(gameObject);
    }
}
