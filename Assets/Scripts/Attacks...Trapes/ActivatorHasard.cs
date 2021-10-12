using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorHasard : MonoBehaviour
{
    [SerializeField] GameObject trigerTrap;
    [SerializeField] float CoolDown;
    BoxCollider ActivationButon;
    float InicialTCooldown;
    float onTime;
    bool activo;
    private void Start()
    {
        ActivationButon = GetComponent<BoxCollider>();
        InicialTCooldown = CoolDown;
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
        CoolDown = InicialTCooldown;
        ActivationButon.enabled = false;
    }
    //maneja los tiempos de activacion tanto de la trampa como el enfriamiento del boton para la trampa
    public void ActvateTrap()
    {
        trigerTrap.SetActive(true);
        onTime -= Time.deltaTime;
        CoolDown -= Time.deltaTime;
        if (onTime <= 0) { trigerTrap.SetActive(false); }
        if (CoolDown <= 0) { activo = false; ActivationButon.enabled = true; }
    }
}
