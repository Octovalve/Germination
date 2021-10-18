using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 NOTA
 Todo esta comentado, por favor leer o preguntar si algo no se entiende
 Este script permite moverse en un tiro parabolico usando el mouse para determinar direccion y fuerza
 Cualquier cosa me preguntan
 ATT: Jesus Antonio Buitrago (Octovalve)
 */

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Drag : MonoBehaviour
{
    private Vector3 mouseDownPos;
    private Vector3 mouseUpPos;
    private Rigidbody rb;
    private bool isShoot = true;
    private Vector3 force;
    TurnControl turnControl;
    [SerializeField] GameObject liquidSlimeBurstVFX;
    [SerializeField] GameObject liquidSlimeVFX;
    ParticleSystem liquidSlimeBurstPs;
    ParticleSystem liquidSlimePs;
    CameraControl camControl;
    Stick stick;
    UIControl ZoomCam;
    [FMODUnity.EventRef]
    public string Event;

    public Vector3 Force { get => force; set => force = value; }
    public bool IsShoot { get => isShoot; set => isShoot = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        turnControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        camControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<CameraControl>();
        liquidSlimeBurstPs = liquidSlimeBurstVFX.GetComponentInChildren<ParticleSystem>();
        liquidSlimePs = liquidSlimeVFX.GetComponentInChildren<ParticleSystem>();
        ZoomCam = camControl.GetComponent<UIControl>();
        stick = GetComponent<Stick>();
    }
    //Toma la posicion del maus en el momento que unde sobr el objeto 
    private void OnMouseDown()
    {
        mouseDownPos = Input.mousePosition;
    }
    //debuelbe la grabedad al objeto toma el bector de la posicion del mouse y corre el void Shoot
    private void OnMouseUp()
    {
        if (turnControl.Estado >= 4)
        {
            Trajectory.Instance.HideLine();
            mouseUpPos = Input.mousePosition;
            force = mouseDownPos - mouseUpPos;
            Shoot(force);
        }
    }
    private void OnMouseDrag()
    {
        Vector3 forceInit = (mouseDownPos - Input.mousePosition);
        if (!isShoot)
        {
            Trajectory.Instance.UpdateTrajectory(forceVector: forceInit * 2000, rb, startingPoint: transform.position);
        }
    }
    public void Jump()
    {
        isShoot = false;
    }
    //determina si el objeto a sido disparado y en caso de que no este marcado como disparado-
    //le agrega una fuersa determinada por la diferencia de los bectores de posicion del muse tomados anterior mente
    void Shoot(Vector3 Force)
    {
        if (isShoot)
        {
            return;
        }
        liquidSlimeBurstPs.Play();
        liquidSlimePs.Play();
        rb.useGravity = true;
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
        rb.AddForce(force * 2000);
        ZoomCam.ZoomCamera1.SetActive(false);
        stick.Landed = false;
        isShoot = true;
    }

}
