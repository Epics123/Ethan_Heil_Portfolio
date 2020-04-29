using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_SceneSwitch : MonoBehaviour
{
    // * Variables *
    public string firstGameScene;
    //public string creditsScene;

    public GameObject controls;
    public GameObject credits;

    public float shrinkSpeed;

    bool creditsOrControls_On = false;
    bool creditControl_switch = true; // True = credits. False = controls

    const int maxScale = 1;
    const int minScale = 0;

    float controlsScale = 0;
    float creditsScale = 0;

    // ** Update Functions **
    private void Update()
    {
        ShowStuff();
    }


    // **** Other Functions ****
    public void StartGame()
    {
        SceneManager.LoadScene(firstGameScene);
    }

    public void ShowCredits()
    {
        if (creditsOrControls_On == false)
        {
            creditsOrControls_On = true;
        }

        creditControl_switch = true;
    }

    public void ShowControls()
    {
        if (creditsOrControls_On == false)
        {
            creditsOrControls_On = true;
        }

        creditControl_switch = false;
    }


    void ShowStuff()
    {
        if (creditsOrControls_On == true)
        {
            switch (creditControl_switch)
            {
                case true:
                    creditsUp(true);
                    controlsUp(false);
                    break;

                case false:
                    creditsUp(false);
                    controlsUp(true);
                    break;
            }
        }
    }

    void creditsUp(bool onOff)
    {
        controlsScale = controls.transform.localScale.y;

        if (onOff == true) // if on
        {
            if (controls.transform.localScale.y < maxScale)
            {
                controlsScale += Time.deltaTime * shrinkSpeed;

                controls.transform.localScale = new Vector3(1, controlsScale, 1);
            } else
            {
                controls.transform.localScale = new Vector3(1, maxScale, 1);
            }
        } 
        else
        {
            if (controls.transform.localScale.y < minScale)
            {
                controlsScale -= Time.deltaTime * shrinkSpeed;

                controls.transform.localScale = new Vector3(1, controlsScale, 1);
            }
            else
            {
                controls.transform.localScale = new Vector3(1, minScale, 1);
            }
        }
    }

    void controlsUp(bool onOff)
    {
        creditsScale = credits.transform.localScale.y;

        if (onOff == true) // if on
        {
            if (credits.transform.localScale.y < maxScale)
            {
                creditsScale += Time.deltaTime * shrinkSpeed;

                credits.transform.localScale = new Vector3(1, creditsScale, 1);
            }
            else
            {
                credits.transform.localScale = new Vector3(1, maxScale, 1);
            }
        }
        else
        {
            if (credits.transform.localScale.y < minScale)
            {
                creditsScale -= Time.deltaTime * shrinkSpeed;

                credits.transform.localScale = new Vector3(1, creditsScale, 1);
            }
            else
            {
                credits.transform.localScale = new Vector3(1, minScale, 1);
            }
        }
    }
}
