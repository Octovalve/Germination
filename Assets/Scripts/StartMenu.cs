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
    public void Credits()
    {
        if (activ == true)
        {
            credits.SetActive(false);
            menuUI.SetActive(true);
            activ = false;
        }
        else if (activ == false)
        {
            credits.SetActive(true);
            menuUI.SetActive(false);
            activ = true;
        }
    }
    public void Options()
    {
        if (activ == true)
        {
            options.SetActive(false);
            menuUI.SetActive(true);
            activ = false;
        }
        else if (activ == false)
        {
            options.SetActive(true);
            menuUI.SetActive(false);
            activ = true;
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(levelName);
    }
}
