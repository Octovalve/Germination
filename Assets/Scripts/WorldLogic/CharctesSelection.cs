using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharctesSelection : MonoBehaviour//Pun//, IPunObservable
{
    [SerializeField] Transform[] team1;
    [SerializeField] Transform[] team2;
    CameraControl cameracontrol;
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
                            cameracontrol.CharacterSelected = curentPlayer.transform;
                            turnControl.Estado += 4;
                        }
                    }
                }
                if (Team == 2)
                {
                    for (int i = 0; i < team2.Length; i++)
                    {
                        if (team2[i] == curentPlayer && turnControl.Estado < 4)
                        {

                            cameracontrol.CharacterSelected = curentPlayer.transform;
                            turnControl.Estado += 4;
                        }
                    }
                }

            }
        }
    }
    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(team);
            Debug.Log(team + "Local");
        }
        else
        {
            this.team = (int)stream.ReceiveNext();
            Debug.Log(team + "Remote");
        }
    }*/
}
