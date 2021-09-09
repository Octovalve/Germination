using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Falingtrap : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    HP hpScript;
    Rigidbody RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            RB.useGravity = true;
            if (collision.gameObject.tag == "Player")
            {
                hpScript = collision.transform.GetComponent<HP>();
                hpScript.TackeDamage(damageToDeal);
                Destroy(gameObject);
                Debug.Log("Attacked :D");
            }
            else
            {
                Debug.Log("Auch :X");
            }
        }
    }
}
