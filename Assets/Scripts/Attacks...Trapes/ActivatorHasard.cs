using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorHasard : MonoBehaviour
{
    [SerializeField] GameObject trigerTrap;
    [SerializeField] int CoolDown;
    int activatorCoolDown;
    BoxCollider ActivationButon;
    TurnControl contador;
    float onTime;
    bool activo;
    private void Start()
    {
        ActivationButon = GetComponent<BoxCollider>();
        contador = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
    }
    private void Update()
    {
        if (activo == true)
        {
            ActvateTrap();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        activo = true;
        onTime = 0.1f;
        ActivationButon.enabled = false;
        activatorCoolDown = contador.ContadorTurno;
        activatorCoolDown += CoolDown;
    }
    //maneja los tiempos de activacion tanto de la trampa como el enfriamiento del boton para la trampa
    public void ActvateTrap()
    {
        trigerTrap.SetActive(true);
        onTime -= Time.deltaTime; 
        if (onTime <= 0) { trigerTrap.SetActive(false); }
        if (CoolDown <= contador.ContadorTurno) { activo = false; ActivationButon.enabled = true; }
    }
}
