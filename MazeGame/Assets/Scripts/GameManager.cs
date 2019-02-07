using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject floor;
    public GameObject[] doors;

    public Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = floor.GetComponent<SpriteRenderer>().color;
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
