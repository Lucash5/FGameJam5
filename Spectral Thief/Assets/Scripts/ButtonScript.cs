using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public GameObject settingsScreen;

    public void buttonPlay()
    {
        SceneManager.LoadScene("Map");
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
