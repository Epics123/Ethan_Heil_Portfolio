using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public FadeManager fadeManager;
    bool pressInput = true;

    // Use this for initialization
    void Start () {
        fadeManager.startFadeIn = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && pressInput)
        {
            pressInput = false;
            fadeManager.startFadeOut = true;
            StartCoroutine(LoadScene());         
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }


}
