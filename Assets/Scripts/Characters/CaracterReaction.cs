using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterReaction : MonoBehaviour
{
    [SerializeField] float knockbackValue;
    Rigidbody thisrb;
    private void Start()
    {
        thisrb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            AddKnockback(collision.gameObject.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            thisrb.useGravity = true;
        }
    }
    public void AddKnockback(Transform BuletHit)
    {
        thisrb.useGravity = true;
        thisrb.AddForce((this.gameObject.transform.position - BuletHit.position) * knockbackValue, ForceMode.Impulse);
        Debug.Log((this.gameObject.transform.position - BuletHit.position));
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            thisrb.useGravity = false;
        }
    }
}
