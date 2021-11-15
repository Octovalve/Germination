using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 NOTA
 Todo esta comentado, por favor leer o preguntar si algo no se entiende
 Este script permite que el objeto se pegue a las superficies al colisionar
 Cualquier cosa me preguntan
 ATT: Jesus Antonio Buitrago (Octovalve)
 */

//Componentes nesesarios para el script
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Stick : MonoBehaviour
{
    TurnControl turnControl;
    //Este string determina el tac se sera el encargado de marcar como aderibles las superficies
    [SerializeField] string StickySurfeceTag;
    [SerializeField] GameObject liquidSlimeVFX;
    SphereCollider ColiderPlayer;
    ParticleSystem liquidSlimePs;
    private bool landed = true;
    private bool grounded = true;
    float tiempoMovimiento = 1f;
    float distPiso;
    [FMODUnity.EventRef]
    public string Event;

    public bool Landed { get => landed; set => landed = value; }
    public float TiempoMovimiento { get => tiempoMovimiento; set => tiempoMovimiento = value; }

    private void Awake()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        liquidSlimePs = liquidSlimeVFX.GetComponentInChildren<ParticleSystem>();
        ColiderPlayer = GetComponent<SphereCollider>();
        distPiso = transform.localScale.magnitude / 2;
    }
    private void Update()
    {
        Grounded();
        if (grounded == true && landed == false)
        {
            tiempoMovimiento -= Time.deltaTime;
            Debug.Log(tiempoMovimiento);
        }
        if (tiempoMovimiento <= 0 && landed == false)
        {
            liquidSlimePs.Stop();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().Sleep();
            if (turnControl.Estado >= 4 && landed == false)
            {
                Debug.Log("detener mov");
                tiempoMovimiento = 1f;
                landed = true;
                turnControl.Estado += 1;
                FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
            }
        }
    }
    //este detiene por completo el movimiento del objeto al colicionar y le quita la gravedad para simular el efecto de que se adiere
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(StickySurfeceTag) || collision.gameObject.CompareTag("jumpingWall"))
        {
            liquidSlimePs.Stop();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().Sleep();
            if (turnControl.Estado >= 4 && landed == false)
            {
                landed = true;
                turnControl.Estado += 1;
                FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
            }
        }
    }
    public void Grounded()
    {
        //Debug.DrawLine(transform.position, transform.position + (Vector3.down * 0.6f));
        if (Physics.Raycast(transform.position, Vector3.down, distPiso + 0.1f))
        {
            grounded = true;
        }
        else { grounded = false; }
    }
}