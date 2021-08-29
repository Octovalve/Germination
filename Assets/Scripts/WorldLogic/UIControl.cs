using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject fondo;
    [SerializeField] Button jumpButon;
    [SerializeField] Button attackButon;
    CharctesSelection teamSelectio;
    TurnControl turnControl;
    //Image fondoColor;
    Drag dragScript;
    //Attack attackScript;
    BoxCollider trigetUI;

    // Start is called before the first frame update
    void Start()
    {
        teamSelectio = GetComponent<CharctesSelection>();
        //fondoColor = fondo.GetComponent<Image>();
        turnControl = GetComponent<TurnControl>();
        trigetUI = GetComponent<BoxCollider>();
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
            //attackScript = teamSelectio.CurentPlayer.GetComponent<Attack>();
        }
        if (turnControl.Estado == 5)
        {
            fondo.SetActive(true);
            jumpButon.interactable = false;
            attackButon.interactable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (turnControl.Estado == 4)
        {
            fondo.SetActive(true);
            jumpButon.interactable = true;
            attackButon.interactable = true;
        }
    }

    public void JumpButon()
    {
        dragScript.IsShoot = false;
        jumpButon.interactable = false;
        trigetUI.enabled = false;
        fondo.SetActive(false);
    }
    public void AttackButon()
    {
        turnControl.Estado += 2;
        attackButon.interactable = false;
    }
}
