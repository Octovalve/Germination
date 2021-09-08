using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BulrtShot : MonoBehaviour
{
    private Vector3 mouseDownPos;
    private Vector3 mouseUpPos;
    private Rigidbody rb;
    private bool isShoot = false;
    private Vector3 force;

    public Vector3 Force { get => force; set => force = value; }
    public bool IsShoot { get => isShoot; set => isShoot = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    //Toma la posicion del maus en el momento que unde sobr el objeto 
    private void OnMouseDown()
    {
        mouseDownPos = Input.mousePosition;
    }
    //debuelbe la grabedad al objeto toma el bector de la posicion del mouse y corre el void Shoot
    private void OnMouseUp()
    {
        Trajectory.Instance.HideLine();
        mouseUpPos = Input.mousePosition;
        force = mouseDownPos - mouseUpPos;
        Shoot(force);
    }
    private void OnMouseDrag()
    {
        Vector3 forceInit = (mouseDownPos - Input.mousePosition);
        if (!isShoot)
        {
            Trajectory.Instance.UpdateTrajectory(forceVector: forceInit * 2, rb, startingPoint: transform.position);
        }
    }
    public void Jump() { isShoot = false; }
    //determina si el objeto a sido disparado y en caso de que no este marcado como disparado-
    //le agrega una fuersa determinada por la diferencia de los bectores de posicion del muse tomados anterior mente
    void Shoot(Vector3 Force)
    {
        if (isShoot)
        {
            return;
        }
        rb.useGravity = true;
        rb.AddForce(force * 2);
        isShoot = true;
    }
}
