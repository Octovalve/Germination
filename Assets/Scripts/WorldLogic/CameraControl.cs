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
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] Camerapan paneo;
    public bool startposreach = false;
    private bool atacked = false;
    private float SmoothFactor;
    private Transform folowThis;
    Transform characterSelected;
    TurnControl turnControl;
    BoxCollider trigetUI;
    int tEspera = 0;

    public Transform FolowThis { get => folowThis; set => folowThis = value; }
    public Transform CharacterSelected { get => characterSelected; set => characterSelected = value; }
    public bool Atacked { get => atacked; set => atacked = value; }
    public float SmoothFactor1 { get => SmoothFactor; set => SmoothFactor = value; }
    public int TEspera { get => tEspera; set => tEspera = value; }

    private void Start()
    {
        turnControl = GetComponent<TurnControl>();
        trigetUI = GetComponent<BoxCollider>();
        paneo = GetComponentInChildren<Camerapan>();
    }
    // LateUpdate is called after Update methods
    private void Update()
    {
        EsperarTiempo();
        if (turnControl.Estado == 4 || turnControl.Estado == 5)
        {
            paneo.enabled = false;
            startposreach = false;
            Vector3 newPos = folowThis.position - cameraOffset;
            this.transform.position = Vector3.Lerp(transform.position, newPos, SmoothFactor1);
        }
        if (turnControl.Estado >= 6 && tEspera == 0)
        {
            paneo.enabled = false;
            startposreach = false;
            Vector3 newPos = CharacterSelected.position - cameraOffset;
            this.transform.position = Vector3.Lerp(transform.position, newPos, SmoothFactor1);
        }
        else if (turnControl.Estado == 0 && tEspera == 0)
        {
            Vector3 newPos = folowThis.position - (cameraOffset+ cameraOffset);
            float distTotal = (newPos - transform.position).magnitude;
            SmoothFactor = 0.1f;
            if (distTotal >= 0.5f && startposreach == false)
            {
                this.transform.position = Vector3.Lerp(transform.position, newPos, SmoothFactor1);
            }
            else
            {
                Debug.Log("Entre");
                paneo.enabled = true;
                startposreach = true;
            }
            Debug.Log(distTotal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SmoothFactor = 1f;
    }
    private void EsperarTiempo()
    {
        if (TEspera > 1)
        {
            TEspera -= 1;
        }
        else { TEspera = 0; }
    }
}
