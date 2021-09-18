using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DamageAttack : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    CameraControl cameracontrol;
    Transform bulletTransform;
    TurnControl turnControl;
    Rigidbody rb;
    HP hpScript;
    private void Start()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        cameracontrol = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<CameraControl>();
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
        cameracontrol.TEspera = 120;
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
