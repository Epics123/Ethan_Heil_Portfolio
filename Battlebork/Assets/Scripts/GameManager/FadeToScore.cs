using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToScore : MonoBehaviour {

    public FadeOut fadeOut;

	// Use this for initialization
	void Start () {
        StartCoroutine("FadeToDeath");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeToDeath()
    {
        yield return new WaitForSeconds(3.0f);
        fadeOut.StartFade();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("ScoreScene");
    }
}
