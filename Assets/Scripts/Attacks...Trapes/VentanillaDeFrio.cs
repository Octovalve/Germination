using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanillaDeFrio : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    [SerializeField] LayerMask layer;
    //envia el daño al jugador (por alguna razon el daño se enbia 2 veces)
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(14.30829f, -5.811924f, 0f), new Vector3(25.63359f / 2, 8.574247f / 2, 1f / 2), Quaternion.identity, layer);
        foreach (Collider nearObject in colliders)
        {
            Debug.Log(nearObject);
            HP hpScript = nearObject.GetComponent<HP>();
            CaracterReaction reactions = nearObject.GetComponent<CaracterReaction>();
            if (hpScript != null)
            {
                hpScript.TackeDamage(damageToDeal);
                reactions.Congelado = true;
                //nearObject.enabled = false;
            }
        }
    }
}
