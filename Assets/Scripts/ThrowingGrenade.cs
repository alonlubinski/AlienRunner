using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingGrenade : MonoBehaviour
{
    public float throwForce = 0.2f;
    public GameObject player;
    public GameObject grenadePrefab;
    public GameObject grenadeInHand;
    public float delay = 3f;
    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q") && countdown == 0f && grenadeInHand.activeSelf)
        {
            ThrowGrenade();
            countdown = delay;
        }
        if(countdown > 0f)
        {
            countdown -= Time.deltaTime;
            Debug.Log(countdown);
            if(countdown <= 0f)
            {
                countdown = 0f;
            }
        }
        
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(player.transform.forward * 10, ForceMode.Impulse);
    }

}
