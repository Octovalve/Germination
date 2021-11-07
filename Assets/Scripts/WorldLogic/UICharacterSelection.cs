using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelection : MonoBehaviour
{
    [SerializeField] Transform[] team1;
    [SerializeField] Transform[] team2;
    CameraControl cameracontrol;
    [FMODUnity.EventRef]
    public string Event;
    private int team;
    TurnControl turnControl;
    private Transform curentPlayer;
    int characterNumber;
    [SerializeField] BoxCollider selectColider;

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
        characterNumber = Mathf.Clamp(characterNumber, 0, 1);
        Team = Mathf.Clamp(Team, 1, 2);
        Debug.Log(curentPlayer);
    }
    private void LateUpdate()
    {
        if (Team == 1)
        {
            curentPlayer = team1[characterNumber].transform;
            for (int i = 0; i < team1.Length; i++)
            {
                if (team1[i] == curentPlayer && turnControl.Estado < 4)
                {
                    cameracontrol.CharacterSelected = curentPlayer.transform;
                    cameracontrol.FolowThis = curentPlayer.transform;
                    turnControl.Estado += 4;
                }
            }
        }
        if (Team == 2)
        {
            curentPlayer = team2[characterNumber].transform;
            for (int i = 0; i < team2.Length; i++)
            {
                if (team2[i] == curentPlayer && turnControl.Estado < 4)
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
    public void SelectionUIadd()
    {
        characterNumber += 1;
        turnControl.Estado = 0;
    }
    public void SelectionUIextract()
    {
        characterNumber -= 1;
        turnControl.Estado = 0;
    }
    public void SelectThis()
    {

    }
}
