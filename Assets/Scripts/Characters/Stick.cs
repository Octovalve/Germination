using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 NOTA
 Todo esta comentado, por favor leer o preguntar si algo no se entiende
 Este script permite que el objeto se pegue a las superficies al colisionar
 Cualquier cosa me preguntan
 ATT: Jesus Antonio Buitrago (Octovalve)
 */

//Componentes nesesarios para el script
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Stick : MonoBehaviour
{
    TurnControl turnControl;
    //Este string determina el tac se sera el encargado de marcar como aderibles las superficies
    [SerializeField] string StickySurfeceTag;
    [FMODUnity.EventRef]
    public string Event;
    private void Awake()
    {
        turnControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
    }
    //este detiene por completo el movimiento del objeto al colicionar y le quita la gravedad para simular el efecto de que se adiere
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(StickySurfeceTag) || collision.gameObject.CompareTag("jumpingWall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().Sleep();
            if (turnControl.Estado >= 4)
            {
                turnControl.Estado += 1;
                FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
            }
        }
    }
}
