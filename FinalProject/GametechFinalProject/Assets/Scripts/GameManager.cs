using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] keys;
    public int numKeys;

    // Start is called before the first frame update
    void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");
        numKeys = keys.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadNextLevel(string level)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(level);
    }
}
