using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnControl : MonoBehaviour
{
    // 4 turno asignado
    // 5 salto
    // 6 ataco
    // 7 termino
    private int estado = 0;
    private int contadorTurno = 1;
    private bool turnStart = false;
    CharctesSelection teamselection;
    [SerializeField] GameObject turnoAzul;
    [SerializeField] GameObject turnoVerde;
    [SerializeField] float turnDuration = 20;
    float turnDurationinicial;
    [SerializeField] Image timeFB;
    UIControl ZoomCam;
    float turnDurationtemp;
    public int Estado { get => estado; set => estado = value; }
    public int ContadorTurno { get => contadorTurno; set => contadorTurno = value; }
    public bool TurnStart { get => turnStart; set => turnStart = value; }

    private void Start()
    {
        turnDurationtemp = turnDuration;
        teamselection = GetComponent<CharctesSelection>();
        ZoomCam = GetComponent<UIControl>();
        turnDurationinicial = turnDuration;

    }
    // Update is called once per frame
    void Update()
    {
        timeFB.fillAmount = turnDuration / turnDurationinicial;
        if (estado >= 7 && teamselection.Team == 1)
        {
            teamselection.Team += 1;
            turnDuration = turnDurationtemp;
            turnStart = false;
            turnoAzul.SetActive(true);
            turnoVerde.SetActive(false);
            estado = 0;
            contadorTurno++;
        }
        else if (estado >= 7 && teamselection.Team == 2)
        {
            teamselection.Team -= 1;
            turnDuration = turnDurationtemp;
            turnStart = false;
            turnoVerde.SetActive(true);
            turnoAzul.SetActive(false);
            estado = 0;
            contadorTurno++;
        }
        if (turnStart == true)
        {
            TurnStarted();
        }
    }
    public void TurnStarted()
    {
        if (turnDuration > 0)
        {
            turnDuration -= Time.deltaTime;
        }
        if (turnDuration <= 0)
        {
            turnStart = false;
            estado = 7;
            ZoomCam.ZoomCamera1.SetActive(false);
            turnDuration = turnDurationtemp;
        }
    }

    public void TurnCanceled()
    {
        turnDuration = turnDurationtemp;
        turnStart = false;
    }
    public void TurnEnded()
    {
        turnStart = false;
        estado = 7;
        turnDuration = turnDurationtemp;
    }
}
