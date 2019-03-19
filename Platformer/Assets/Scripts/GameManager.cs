using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public Transform restartPoint;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = restartPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRestart();
    }


    void CheckRestart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadNextLevel(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void KillPlayer()
    {
        player.transform.position = restartPoint.position;
        player.transform.rotation = restartPoint.rotation;
    }
}
