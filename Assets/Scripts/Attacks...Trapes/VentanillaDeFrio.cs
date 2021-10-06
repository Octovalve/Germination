using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanillaDeFrio : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    HP hpScript;
    //envia el daño al jugador (por alguna razon el daño se enbia 2 veces)
    private void OnTriggerEnter(Collider other)
    {
        hpScript = other.transform.GetComponent<HP>();
        hpScript.TackeDamage(damageToDeal);
    }
}
