using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //efecto o animacion o algo visual:D
            Destroy(collision.gameObject);
        }
        else { Destroy(collision.gameObject); }
    }
}
