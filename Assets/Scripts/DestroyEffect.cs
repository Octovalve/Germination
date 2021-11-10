using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    ParticleSystem[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                if (!ps.isPlaying && ps != null)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
