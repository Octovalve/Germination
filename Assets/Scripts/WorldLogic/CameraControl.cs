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

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform camStartPos;
    [SerializeField] bool LookAtPlayer;
    [SerializeField] Vector3 cameraOffset;
    private float SmoothFactor;
    private bool atacked = false;
    private Transform characterSelected;
    TurnControl turnControl;
    BoxCollider trigetUI;

    public Transform CharacterSelected { get => characterSelected; set => characterSelected = value; }
    public bool Atacked { get => atacked; set => atacked = value; }
    public float SmoothFactor1 { get => SmoothFactor; set => SmoothFactor = value; }

    private void Start()
    {
        turnControl = GetComponent<TurnControl>();
        trigetUI = GetComponent<BoxCollider>();
    }
    // LateUpdate is called after Update methods
    private void Update()
    {
        if (turnControl.Estado >= 4)
        {
            Vector3 newPos = characterSelected.position - cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor1);
            if (LookAtPlayer) { transform.LookAt(characterSelected); }
        }
        else if (turnControl.Estado == 0)
        {
            SmoothFactor = 0.05f;
            Vector3 newPos = camStartPos.position - cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor1);
            trigetUI.enabled = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SmoothFactor = 1f;
    }
}
