using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LataE : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    [SerializeField] LayerMask layer;
    [SerializeField] float force;
    [SerializeField] float radio;
    [SerializeField] int explotarEnRonda;
    ParticleSystem liquidSlimeBurstPs;
    BoxCollider pltcollider;
    TurnControl contador;
    float delay = 2f;
    Vector3 collider_center, collider_size;
    // Start is called before the first frame update
    void Start()
    {
        pltcollider = GetComponent<BoxCollider>();
        collider_center = pltcollider.bounds.center;
        collider_size = pltcollider.bounds.size;
        liquidSlimeBurstPs = GetComponentInChildren<ParticleSystem>();
        contador = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (contador.ContadorTurno >= explotarEnRonda) { Explotar(); }
    }
    public void Explotar()
    {
        liquidSlimeBurstPs.Play();
        delay -= Time.deltaTime;
        Collider[] colliders = Physics.OverlapBox(collider_center, (collider_size / 2.0f), Quaternion.identity, 1 << 8);
        foreach (Collider nearObject in colliders)
        {
            Debug.Log(nearObject);
            HP hpScript = nearObject.GetComponent<HP>();
            Rigidbody rb = nearObject.GetComponent<Rigidbody>();
            CaracterReaction reactions = nearObject.GetComponent<CaracterReaction>();
            if (hpScript != null)
            {
                hpScript.TackeDamage(damageToDeal);
                rb.AddExplosionForce(force * 2000, collider_center, radio);
                rb.useGravity = true;
                reactions.Congelado = true;
            }
        }
        if (delay <= 0) { Destroy(gameObject); }
    }
}
