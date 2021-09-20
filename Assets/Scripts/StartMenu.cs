using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject credits;
    [SerializeField] GameObject options;
    [SerializeField] GameObject menuUI;
    [SerializeField] string levelName;
    bool activ = false;
    [FMODUnity.EventRef]
    public string CancelBSound;
    [FMODUnity.EventRef]
    public string OkButtonSound;

    public void Credits()
    {
        if (activ == true)
        {
            credits.SetActive(false);
            menuUI.SetActive(true);
            activ = false;
            FMODUnity.RuntimeManager.PlayOneShotAttached(CancelBSound, gameObject);
        }
        else if (activ == false)
        {
            credits.SetActive(true);
            menuUI.SetActive(false);
            activ = true;
            FMODUnity.RuntimeManager.PlayOneShotAttached(OkButtonSound, gameObject);
        }
    }
    public void Options()
    {
        if (activ == true)
        {
            options.SetActive(false);
            menuUI.SetActive(true);
            activ = false;
            FMODUnity.RuntimeManager.PlayOneShotAttached(CancelBSound, gameObject);
        }
        else if (activ == false)
        {
            options.SetActive(true);
            menuUI.SetActive(false);
            activ = true;
            FMODUnity.RuntimeManager.PlayOneShotAttached(OkButtonSound, gameObject);
        }
    }
    public void Play()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(OkButtonSound, gameObject);
        SceneManager.LoadScene(levelName);
    }
}
