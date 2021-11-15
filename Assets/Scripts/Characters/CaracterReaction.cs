using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterReaction : MonoBehaviour
{
    [SerializeField] float knockbackValue;
    [SerializeField] int Rondascongelado;
    [SerializeField] GameObject freezeEffect;
    GameObject freezeVFX;
    int turnoscongelado;
    TurnControl contador;
    SphereCollider colliderobj;
    Rigidbody thisrb;
    private bool congelado = false;
    int state;

    public bool Congelado { get => congelado; set => congelado = value; }

    private void Start()
    {
        thisrb = GetComponent<Rigidbody>();
        contador = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        colliderobj = GetComponent<SphereCollider>();
        state = 0;
    }
    private void Update()
    {
        if (congelado == true) { 
            EstadoCongelado();

            if (state == 0)
            {
                state = 1;
            }
        }
        if (congelado == false && contador.ContadorTurno == turnoscongelado) { 
            //colliderobj.enabled = true;

            if (state == 2)
            {
                Destroy(freezeVFX);
                state = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            AddKnockback(collision.gameObject.transform);
        }
    }
    public void AddKnockback(Transform BuletHit)
    {
        Vector3 colitionDirection = this.transform.position - BuletHit.position;
        Vector3 forceDirection = new Vector3(colitionDirection.x, colitionDirection.y * -1, colitionDirection.z);
        thisrb.AddForce((forceDirection * knockbackValue) * 2000, ForceMode.Impulse);
        thisrb.useGravity = true;
    }
    public void EstadoCongelado()
    {
        if (congelado == true && turnoscongelado < contador.ContadorTurno) { turnoscongelado += (contador.ContadorTurno + Rondascongelado); }
        if (contador.ContadorTurno >= turnoscongelado) { congelado = false; }

        if (state == 1)
        {
            freezeVFX = Instantiate(freezeEffect, transform.position, Quaternion.identity) as GameObject;
            state = 2;
        }
    }
}
