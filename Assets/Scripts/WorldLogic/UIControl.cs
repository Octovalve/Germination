using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] string levelToLoad;
    [SerializeField] GameObject fondo;
    [SerializeField] Button jumpButon;
    [SerializeField] Button spitButon;
    [SerializeField] Button pasturn;
    [SerializeField] Button cancel;
    CharctesSelection teamSelectio;
    CameraControl cameracontrol;
    TurnControl turnControl;
    BoxCollider trigerUI;
    Attack attackScript;
    Drag dragScript;
    int laststate;
    // Start is called before the first frame update
    void Start()
    {
        teamSelectio = GetComponent<CharctesSelection>();
        turnControl = GetComponent<TurnControl>();
        cameracontrol = GetComponent<CameraControl>();
        trigerUI = GetComponent<BoxCollider>();
        jumpButon.interactable = false;
        spitButon.interactable = false;
        pasturn.interactable = false;
        cancel.interactable = false;
    }
    private void Update()
    {
        if (turnControl.Estado == 0)
        {
            trigerUI.enabled = true;
            fondo.SetActive(false);
        }

        if (turnControl.Estado != laststate && turnControl.Estado < 7)
        {
            fondo.SetActive(true);
            laststate = turnControl.Estado;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (turnControl.Estado == 4)
        {
            dragScript = teamSelectio.CurentPlayer.GetComponent<Drag>();
            attackScript = teamSelectio.CurentPlayer.GetComponent<Attack>();
            fondo.SetActive(true);
            jumpButon.interactable = true;
            spitButon.interactable = true;
            pasturn.interactable = true;
            cancel.interactable = true;
            trigerUI.enabled = false;
        }
    }
    public void Weapon1()
    {
        attackScript.Attack1();
        spitButon.interactable = false;
        cancel.interactable = false;
        fondo.SetActive(false);
        laststate = turnControl.Estado;
    }
    public void Weapon2()
    {
        attackScript.Attack3();
        //spitButon.interactable = false;
        cancel.interactable = false;
        fondo.SetActive(false);
        laststate = turnControl.Estado;
    }
    public void Weapon3()
    {
        attackScript.Attack3();
        //spitButon.interactable = false;
        cancel.interactable = false;
        fondo.SetActive(false);
        laststate = turnControl.Estado;
    }
    public void JumpButon()
    {
        dragScript.IsShoot = false;
        jumpButon.interactable = false;
        cancel.interactable = false;
        fondo.SetActive(false);
        laststate = turnControl.Estado;
    }
    public void Cancel()
    {
        turnControl.Estado = 0;
    }
    public void EndTurn()
    {
        jumpButon.interactable = false;
        spitButon.interactable = false;
        pasturn.interactable = false;
        cancel.interactable = false;
        turnControl.Estado = 7;
    }
    public void Reload()
    {
        SceneManager.LoadScene(levelToLoad);
        Time.timeScale = 1;
    }

}
