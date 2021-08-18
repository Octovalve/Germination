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
    [SerializeField] Transform[] team1;
    [SerializeField] Transform[] team2;
    [SerializeField] int playerNumber;
    [SerializeField] int team;
    [SerializeField] bool LookAtPlayer;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] [Range(0.01f, 1.0f)] float SmoothFactor;

    private void Update()
    {
        playerNumber = Mathf.Clamp(playerNumber, 0, team1.Length - 1);
        team = Mathf.Clamp(team, 1, 2);
    }
    // LateUpdate is called after Update methods
    private void LateUpdate()
    {
        if (team == 1)
        {
            Vector3 newPos = team1[playerNumber].position - cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
            if (LookAtPlayer) { transform.LookAt(team1[playerNumber]); }
        }
        if (team == 2)
        {
            Vector3 newPos = team2[playerNumber].position - cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
            if (LookAtPlayer) { transform.LookAt(team2[playerNumber]); }
        }
    }
}
