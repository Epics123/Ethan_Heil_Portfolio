using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject floor;
    public GameObject player;
    public GameObject[] doors;

    public Color originalColor;

    int startLayer = 8;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = floor.GetComponent<SpriteRenderer>().color;
        floor.layer = startLayer;
        player.layer = startLayer;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
