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
    [FMODUnity.EventRef]
    public string Event;
    private void Start()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TurnControl>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
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
