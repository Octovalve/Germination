using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damgetest : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    HP hpScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hpScript = collision.transform.GetComponent<HP>();
            hpScript.TackeDamage(damageToDeal);
            Debug.Log("Attacked :D");
        }
        else
        {
            Debug.Log("Auch :X");
        }
    }
}
