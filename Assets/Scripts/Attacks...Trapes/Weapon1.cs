﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{
    [SerializeField] float ofsetRot;
    [SerializeField] Transform SpawnP;
    [SerializeField] GameObject projectile;
    Vector3 mouseUpPos;
    Vector3 force;
    CameraControl camControl;
    Rigidbody rb;
    Camera cam;
    float forceMod = 2;
    [FMODUnity.EventRef]
    public string Event;
    private bool isShoot = false;

    public bool IsShoot { get => isShoot; set => isShoot = value; }

    private void Start()
    {
        rb = projectile.GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
    }
    void Update()
    {
        Vector3 diference = transform.position - WorldPosition(0);
        force = SpawnP.position - WorldPosition(0);
        float rotZ = Mathf.Atan2(-diference.y, -diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + ofsetRot);
        if (Input.GetMouseButton(0))
        {
            if (IsShoot == false)
            {
                Trajectory.Instance.UpdateTrajectoryInpuls(forceVector: force * forceMod, rb, startingPoint: SpawnP.position);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot(force);
        }

    }
    void Shoot(Vector3 Force)
    {
        if (IsShoot) { return; }
        Trajectory.Instance.HideLine();
        GameObject bullet = Instantiate(projectile, SpawnP.position, Quaternion.identity) as GameObject;
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
        camControl.FolowThis = bullet.GetComponent<Transform>();
        bullet.GetComponent<Rigidbody>().AddForce(force * forceMod, ForceMode.Impulse);
        IsShoot = true;
    }
    private Vector3 WorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}
