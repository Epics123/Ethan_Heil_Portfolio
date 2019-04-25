using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string nextLevel;

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(nextLevel);
    }
}
