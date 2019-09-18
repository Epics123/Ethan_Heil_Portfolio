using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    public FadeManager fadeManager;

    // Use this for initialization
    void Start () {
        fadeManager.startFadeIn = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator LoadScene()
    {
        fadeManager.startFadeOut = true;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}
