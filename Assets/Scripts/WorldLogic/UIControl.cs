using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject fondo;
    [SerializeField] Button jumpButon;
    [SerializeField] Button spitButon;
    BoxCollider trigetUI;

    CharctesSelection teamSelectio;
    CameraControl cameracontrol;
    TurnControl turnControl;
    Attack attackScript;
    Drag dragScript;
    int laststate;
    // Start is called before the first frame update
    void Start()
    {
        teamSelectio = GetComponent<CharctesSelection>();
        turnControl = GetComponent<TurnControl>();
        trigetUI = GetComponent<BoxCollider>();
        cameracontrol = GetComponent<CameraControl>();
        jumpButon.interactable = false;
        spitButon.interactable = false;
    }
    private void Update()
    {
        Debug.Log(turnControl.Estado);
        if (turnControl.Estado == 0)
        {
            trigetUI.enabled = true;
            fondo.SetActive(false);
        }
        if (turnControl.Estado == 4)
        {
            dragScript = teamSelectio.CurentPlayer.GetComponent<Drag>();
            attackScript = teamSelectio.CurentPlayer.GetComponent<Attack>();
        }
        if (turnControl.Estado != laststate && turnControl.Estado <7)
        {
            fondo.SetActive(true);
            laststate = turnControl.Estado;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (turnControl.Estado == 4)
        {
            fondo.SetActive(true);
            jumpButon.interactable = true;
            spitButon.interactable = true;
        }
    }
    public void Weapon1()
    {
        Debug.Log("Spit");
        spitButon.interactable = false;
        trigetUI.enabled = false;
        fondo.SetActive(false);
        laststate = turnControl.Estado;
    }
    public void Reload()
    {
        SceneManager.LoadScene("MovimientoParabolico");
        Time.timeScale = 1;
    }
    public void JumpButon()
    {
        dragScript.IsShoot = false;
        jumpButon.interactable = false;
        trigetUI.enabled = false;
        fondo.SetActive(false);
        laststate = turnControl.Estado;
    }
}
