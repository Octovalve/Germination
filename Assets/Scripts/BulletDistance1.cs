using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDistance1 : MonoBehaviour
{
    Vector3 initialPos;
    [SerializeField] float maxDistance;
    [SerializeField] GameObject hitVFX;
    CameraControl cameracontrol;
    TurnControl turnControl;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        turnControl = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<TurnControl>();
        cameracontrol = GameObject.FindGameObjectWithTag("MainCinemachineCamera").GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(initialPos, transform.position);

        if (dist >= maxDistance)
        {
            GameObject hit = Instantiate(hitVFX, transform.position, Quaternion.identity) as GameObject;
           
            Destroy(gameObject);
        }
    }
}
