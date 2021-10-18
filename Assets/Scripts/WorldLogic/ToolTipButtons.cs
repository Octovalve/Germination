using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipButtons : MonoBehaviour
{
    [SerializeField] int HowManyTips;
    [TextArea] public string[] tipText;

    //[SerializeField] TextMeshProUGUI tip;
    //[SerializeField] GameObject tipImage;
    int currentTip;

    [SerializeField] TextMeshProUGUI[] tipsArray;
    [SerializeField] GameObject[] TipsImageArray;

    [SerializeField] LayoutElement layoutElement = null;
    [SerializeField] int characterWrapLimit = 0;

    [SerializeField] Button buton;

    void Start()
    {
        currentTip = 0;
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
        yield return new WaitForSeconds(2f);

        tipsArray[currentTip].text = "";
        TipsImageArray[currentTip].SetActive(false);

        //tipImage.SetActive(false);
        //tip.text = "";

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
