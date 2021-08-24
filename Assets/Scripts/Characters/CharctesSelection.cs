using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharctesSelection : MonoBehaviour
{
    [SerializeField] Transform[] team1;
    [SerializeField] Transform[] team2;
    CameraControl cameracontrol;
    [SerializeField] int team;


    // Start is called before the first frame update
    void Start()
    {
        cameracontrol = GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        team = Mathf.Clamp(team, 1, 2);
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Transform target = hit.transform;
                if (team == 1)
                {
                    for (int i = 0; i < team1.Length; i++)
                    {
                        if (team1[i] == target)
                        {
                            cameracontrol.Selecting = false;
                            cameracontrol.CharacterSelected = target.transform;
                        }
                    }
                }
                if (team == 2)
                {
                    for (int i = 0; i < team2.Length; i++)
                    {
                        if (team2[i] == target)
                        {
                            cameracontrol.Selecting = false;
                            cameracontrol.CharacterSelected = target.transform;
                        }
                    }
                }
            }
        }
    }
}
