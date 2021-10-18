using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterReaction : MonoBehaviour
{
    [SerializeField] float knockbackValue;
    [SerializeField] int Rondascongelado;
    int turnoscongelado;
    TurnControl contador;
    SphereCollider collider;
    Rigidbody thisrb;
    private bool congelado = false;

    public bool Congelado { get => congelado; set => congelado = value; }

    private void Start()
    {
        thisrb = GetComponent<Rigidbody>();
        contador = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        collider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        if (congelado == true) { EstadoCongelado(); }
        if (congelado == false && contador.ContadorTurno == turnoscongelado) { collider.enabled = true; }
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
        Vector3 forceDirection = new Vector3(colitionDirection.x,colitionDirection.y*-1,colitionDirection.z); 
        thisrb.AddForce((forceDirection*knockbackValue) * 2000, ForceMode.Impulse);
        thisrb.useGravity = true;
        Debug.Log(forceDirection + "Direcion fuerza");
        Debug.Log(colitionDirection + "Dierecion colicion");
    }
    public void EstadoCongelado()
    {
        if (congelado == true && turnoscongelado < contador.ContadorTurno) { turnoscongelado += (contador.ContadorTurno + Rondascongelado); }
        if (contador.ContadorTurno >= turnoscongelado) { congelado = false; }
    }
}
