using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharctesSelection : MonoBehaviour
{
    [SerializeField] Transform[] team1;
    [SerializeField] Transform[] team2;
    CameraControl cameracontrol;
    CaracterReaction reaction;
    [FMODUnity.EventRef]
    public string Event;
    private int team;
    TurnControl turnControl;
    private Transform curentPlayer;

    public int Team { get => team; set => team = value; }
    public Transform CurentPlayer { get => curentPlayer; set => curentPlayer = value; }


    // Start is called before the first frame update
    void Start()
    {
        cameracontrol = GetComponent<CameraControl>();
        turnControl = GetComponent<TurnControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Team = Mathf.Clamp(Team, 1, 2);
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                curentPlayer = hit.transform;
                if (Team == 1)
                {
                    for (int i = 0; i < team1.Length; i++)
                    {
                        if (team1[i] == curentPlayer && turnControl.Estado < 4)
                        {
                            reaction = curentPlayer.GetComponent<CaracterReaction>();
                            if (reaction.Congelado == false)
                            {
                                FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
                                cameracontrol.CharacterSelected = curentPlayer.transform;
                                cameracontrol.FolowThis = curentPlayer.transform;
                                turnControl.Estado += 4;
                                turnControl.TurnStart = true;
                            }
                        }
                    }
                }
                if (Team == 2)
                {
                    for (int i = 0; i < team2.Length; i++)
                    {
                        if (team2[i] == curentPlayer && turnControl.Estado < 4)
                        {
                            reaction = curentPlayer.GetComponent<CaracterReaction>();
                            if (reaction.Congelado == false)
                            {
                                FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
                                cameracontrol.CharacterSelected = curentPlayer.transform;
                                cameracontrol.FolowThis = curentPlayer.transform;
                                turnControl.Estado += 4;
                                turnControl.TurnStart = true;
                            }
                        }
                    }
                }

            }
        }
    }
}
