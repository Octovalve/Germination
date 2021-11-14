using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerapan : MonoBehaviour
{
    [SerializeField] BoxCollider Limites;
    [SerializeField] GameObject cam;
    [SerializeField] Transform Cameralimits;
    [SerializeField] Camera cameraWorld;
    [SerializeField] float groundZ = 0;
    private Vector3 startpoint;
    private Vector3 posicionPaneo;
    private Vector3 paneoControler;
    private void Start()
    {
        paneoControler = Cameralimits.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startpoint = WorldPosition(groundZ);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = startpoint - WorldPosition(0);
            cam.transform.position += direction;
        }
    }
    private void LateUpdate()
    {
        posicionPaneo = cam.transform.position;
        posicionPaneo.x = Mathf.Clamp(posicionPaneo.x, (Limites.bounds.extents.x * -1) + paneoControler.x, Limites.bounds.extents.x + paneoControler.x);
        posicionPaneo.y = Mathf.Clamp(posicionPaneo.y, (Limites.bounds.extents.y * -1) + paneoControler.y, Limites.bounds.extents.y + paneoControler.y);
        cam.transform.position = posicionPaneo;
    }
    private Vector3 WorldPosition(float z)
    {
        Ray mousePos = cameraWorld.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}