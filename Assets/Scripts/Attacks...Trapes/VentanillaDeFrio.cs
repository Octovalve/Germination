using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanillaDeFrio : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    [SerializeField] LayerMask layer;
    BoxCollider pltcollider;
    Vector3 collider_center, collider_size;
    [FMODUnity.EventRef]
    public string FreezeSound;
    private void Start()
    {
        pltcollider = GetComponent<BoxCollider>();
        collider_center = pltcollider.bounds.center;
        collider_size = pltcollider.bounds.size;
    }
    //envia el daño al jugador (por alguna razon el daño se enbia 2 veces)
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapBox(collider_center, (collider_size / 2.0f), Quaternion.identity, 1 << 8);
        foreach (Collider nearObject in colliders)
        {
            HP hpScript = nearObject.GetComponent<HP>();
            FMODUnity.RuntimeManager.PlayOneShotAttached(FreezeSound, gameObject);
            CaracterReaction reactions = nearObject.GetComponent<CaracterReaction>();
            if (hpScript != null)
            {
                hpScript.TackeDamage(damageToDeal);
                reactions.Congelado = true;
                nearObject.enabled = false;
            }
        }
    }
}