using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Falingtrap : MonoBehaviour
{
    [SerializeField] float damageToDeal;
    HP hpScript;
    Rigidbody RB;
    [FMODUnity.EventRef]
    public string SpikeSound;
    public bool spikeActivado;

    IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" && spikeActivado == true)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(SpikeSound, gameObject);
            StartCoroutine("Desaparecer");
            Debug.Log("Auch :X");
        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "shot")
        {
            RB.useGravity = true;
            spikeActivado = true;
            if (collision.gameObject.tag == "Player")
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(SpikeSound, gameObject);
                hpScript = collision.transform.GetComponent<HP>();
                hpScript.TackeDamage(damageToDeal);
                Destroy(gameObject);
                Debug.Log("Attacked :D");
            }

        }
    }
}
