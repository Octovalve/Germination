using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelToolTip : MonoBehaviour
{
    [SerializeField] GameObject cancelButton;

    [SerializeField] GameObject toolTip;

    void Start()
    {
        cancelButton.SetActive(false);
    }

    public void Cancel()
    {
        toolTip.SetActive(false);
        StartCoroutine(ReStart());
    }

    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(0.5f);
        toolTip.SetActive(true);
    }
}
