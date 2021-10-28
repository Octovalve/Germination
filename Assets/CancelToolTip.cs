using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelToolTip : MonoBehaviour
{
    [SerializeField] GameObject toolTip;

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
