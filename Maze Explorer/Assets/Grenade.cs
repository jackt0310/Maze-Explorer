﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 20f;
    public float force = 7000f;

    public float explosionSize = 10f;

    public GameObject explosionEffect;

    float countdown;
    bool isExploded = false;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !isExploded)
        {

            Explode();
            isExploded = true;
        }
    }

    void Explode()
    {
        GameObject explosionObject = Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject;
        explosionObject.transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
        Destroy(explosionObject, 1.9f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        Destroy(gameObject);
    }
}
