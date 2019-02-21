using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    bool canHitKey;
    // Start is called before the first frame update
    void Start()
    {
        canHitKey = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canHitKey)
        {
            SceneManager.LoadSceneAsync(1);
            canHitKey = false;
        }
    }
}
