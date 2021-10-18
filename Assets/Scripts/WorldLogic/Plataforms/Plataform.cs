﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    //[SerializeField] int layernumber = 8;
    Rigidbody RB;
    BoxCollider pltcollider;
    float wait;
    bool hit = false;
    Vector3 collider_center, collider_size;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        pltcollider = GetComponent<BoxCollider>();
        collider_center = pltcollider.bounds.center;
        collider_size = pltcollider.bounds.size;
    }
    private void Update()
    {
        if (hit == true) { wait -= Time.deltaTime; }
        if (wait <= 0 && hit == true) { gameObject.SetActive(false); }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            pltcollider.enabled = false;
            RB.isKinematic = false;
            wait = 0.5f;
            hit = true;
            Destroid();
        }
    }
    public void Destroid()
    {
        Collider[] colliders = Physics.OverlapBox(collider_center, (collider_size / 2.0f) * 1.3f, Quaternion.identity, 1 << 8);
        foreach (Collider nearObject in colliders)
        {
            Rigidbody rb = nearObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
            }
        }
    }
}
