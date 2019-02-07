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

    public int startLevel = 1;
    public int levelIdx;

    public enum Levels
    {
        LEVEL1,
        LEVEL2,
        LEVEL3
    }

    Levels currentLevel = Levels.LEVEL1;

    int startLayer = 8;

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

    public void ChangeLevel(int level)
    {
        switch(level)
        {
            case 3: currentLevel = Levels.LEVEL3;
                break;
            case 2: currentLevel = Levels.LEVEL2;
                break;
            default: currentLevel = Levels.LEVEL1;
                break;
        }
    }

    public Levels GetCurrentLevel()
    {
        return currentLevel;
    }

    public void IncreaseLevel()
    {
        levelIdx++;
    }
}
