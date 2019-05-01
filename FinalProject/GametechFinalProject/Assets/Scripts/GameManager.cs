using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] keys;
    public Transform spawnPoint;
    public GameObject player;
    public Text keyText;
    public int numKeys;
    public int playerKeys = 0;

    // Start is called before the first frame update
    void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");
        numKeys = keys.Length;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
        keyText.text = playerKeys.ToString() + "/" + keys.Length.ToString();
    }

    public IEnumerator LoadNextLevel(string level)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(level);
    }
}
