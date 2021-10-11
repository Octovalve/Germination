using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    //[SerializeField] int layernumber = 8;
    Rigidbody RB;
    Collider collider;
    float wait;
    bool hit = false;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
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
            collider.enabled = false;
            RB.isKinematic = false;
            wait = 0.5f;
            hit = true;
            Destroid();
        }
    }
    public void Destroid()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0.28f, 0.08f, 1), new Vector3(2.543038f / 2, 0.91f / 2, 11.27829f / 2), Quaternion.identity, 1 << 8);
        foreach (Collider nearObject in colliders)
        {
            Rigidbody rb = nearObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                Debug.Log(rb);
            }
        }
    }
}
