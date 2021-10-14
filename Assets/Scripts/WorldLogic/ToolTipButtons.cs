using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipButtons : MonoBehaviour
{
    [TextArea] public string tipText;

    [SerializeField] TextMeshProUGUI tip;
    [SerializeField] GameObject tipImage;

    [SerializeField] LayoutElement layoutElement = null;
    [SerializeField] int characterWrapLimit = 0;

    public void ShowTip()
    {
        int contentLenght = tip.text.Length;

        layoutElement.enabled = (contentLenght > characterWrapLimit) ? true : false;

        tipImage.SetActive(true);
        tip.text = "" + tipText;
        StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(4f);
        tipImage.SetActive(false);
        tip.text = "";
    }
}
