using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition1;
    public Animator transition2;
    public float transitionTime = 1f;
    public int nextLevel;

    public CanvasGroup canvasGroup1;
    public CanvasGroup canvasGroup2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(nextLevel));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        canvasGroup1.alpha = 1f;
        canvasGroup2.alpha = 1f;

        transition1.SetTrigger("Start");
        transition2.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
