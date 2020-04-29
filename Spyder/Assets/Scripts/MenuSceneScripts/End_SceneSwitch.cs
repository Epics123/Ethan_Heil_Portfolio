using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_SceneSwitch : MonoBehaviour
{
    // * Variables *
    public string StartScene;

    // ** Update Functions **


    // **** Other Functions ****
    public void ReturnToStart()
    {
        SceneManager.LoadScene(StartScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
