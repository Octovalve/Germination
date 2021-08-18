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
    public bool isShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        mouseDownPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
        mouseUpPos = Input.mousePosition;
        Shoot(Force: mouseDownPos - mouseUpPos);
    }

    void Shoot(Vector3 Force)
    {
        if (isShoot)
        {
            return;
        }
        rb.AddForce(new Vector3(Force.x * 2, Force.y * 2, z: Force.y * 2));
        isShoot = true;
    }
}
