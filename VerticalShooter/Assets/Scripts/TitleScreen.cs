using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script for handling title screen behavior
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

    //Check for player input
    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canHitKey)
        {
            //Load game scene
            SceneManager.LoadSceneAsync(1);
            canHitKey = false;
        }
    }
}
