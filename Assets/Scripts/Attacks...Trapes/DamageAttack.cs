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
    private void Start()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TurnControl>();
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
            if (turnControl.Estado >= 4)
            {
                turnControl.Estado += 2;
            }
            Destroy(gameObject);
        }
    }
}
