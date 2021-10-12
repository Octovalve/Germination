using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    Rigidbody RB;
    float wait;
    bool hit = false;
    Rigidbody PRB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
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
            RB.isKinematic = false;
            wait = 0.5f;
            hit = true;
        }
    }
}
