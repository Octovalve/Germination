using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DamageAttack : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    TurnControl turnControl;
    HP hpScript;
    Rigidbody rb;
    Transform bulletTransform;
    private void Start()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TurnControl>();
        rb = GetComponent<Rigidbody>();
        bulletTransform = GetComponentInChildren<Transform>();
    }
    private void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        bulletTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hpScript = collision.transform.GetComponent<HP>();
            hpScript.TackeDamage(damageToDeal);
            if (turnControl.Estado >= 4)
            {
                turnControl.Estado += 2;
            }
            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.tag != "jumpingWall")
            {
                if (turnControl.Estado >= 4)
                {
                    turnControl.Estado += 2;
                }
                Destroy(gameObject);
            }
            /*if (turnControl.Estado >= 4)
            {
                turnControl.Estado += 2;
            }
            Destroy(gameObject);*/
        }
    }
}
