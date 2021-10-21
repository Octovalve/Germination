using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipButtons : MonoBehaviour
{
    [SerializeField] bool automatic;
    [SerializeField] bool atTurnEnd;
    [SerializeField] float delay;

    [SerializeField] int HowManyTips;
    [TextArea] public string[] tipText;

    int currentTip;

    [SerializeField] TextMeshProUGUI[] tipsArray;
    [SerializeField] GameObject[] TipsImageArray;

    [SerializeField] LayoutElement layoutElement = null;
    [SerializeField] int characterWrapLimit = 0;

    [SerializeField] Button buton;

    void OnEnable()
    {
        UIControl.EndTurnAction += TurnEnd;
    } 

    void OnDisable()
    {
        UIControl.EndTurnAction -= TurnEnd;

        for (int i = 0; i < tipsArray.Length; i++)
        {
            tipsArray[currentTip].text = "";
            TipsImageArray[currentTip].SetActive(false); 
        }

        currentTip = 0;

        buton.enabled = true;
    }

    void Start()
    {
        currentTip = 0;

        if (automatic == true)
        {
            ShowTip();
        }
    }

    void TurnEnd()
    {
        if(atTurnEnd == true)
        {
            currentTip = 0;
            ShowTip();
        }
    }

    public void ShowTip()
    {
        int contentLenght = tipsArray[currentTip].text.Length;

        layoutElement.enabled = (contentLenght > characterWrapLimit) ? true : false;

        TipsImageArray[currentTip].SetActive(true);
        tipsArray[currentTip].text = "" + tipText[currentTip];

        buton.enabled = false;

        StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(delay);

        tipsArray[currentTip].text = "";
        TipsImageArray[currentTip].SetActive(false);

        if (currentTip != HowManyTips - 1)
        {
            currentTip += 1;
            ShowTip();
        }
        else
        {
            currentTip = 0;
            buton.enabled = true;
        }
    }
}
