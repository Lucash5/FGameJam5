using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public GameObject settingsScreen;

    private void Start()
    {
        settingsScreen.SetActive(false);
    }
    public void buttonPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("LEVEL LOADED LOL!!!!!!!!!1111");
    }
    public void buttonQuit()
    {
        Application.Quit();
        Debug.Log("QUITTEDSDUAFHERHJHOLUKLJIOÖ");
    }

    public void buttonSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void buttonSettingsQuit()
    {
        settingsScreen.SetActive(false);
    }
}
