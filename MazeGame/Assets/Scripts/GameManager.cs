using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject floor;
    public GameObject player;
    public GameObject spawnPoint;
    public GameObject[] doors;
    public Color originalColor;

    public int startLevel = 1;
    public int levelIdx;
    public int startLayer = 8;
    public int lvl2Layer = 9;
    public int lvl3Layer = 10;


    // Start is called before the first frame update
    void Start()
    {
        originalColor = floor.GetComponent<SpriteRenderer>().color;
        floor.layer = startLayer;
        player.layer = startLayer;
        levelIdx = startLevel;
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
