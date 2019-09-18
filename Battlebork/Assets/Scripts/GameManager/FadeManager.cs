using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

    public FadeOut fadeOut;
    public GameManager gm;
    int counter;
    int count;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        } 
    }

    // Use this for initialization
    void Start()
    {
        counter = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name != "GameScene")
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (counter >= 1)
                {
                    return;
                }
                else if(counter < 1)
                {
                    fadeOut.StartFade();
                    StartCoroutine("ChangeToGameScene");
                }
                counter++;
            }
            
        }
        
        if(gm != null && gm.getLives() <= 0)
        {
            if(count <= 1)
            {
                fadeOut.StartFade();
                StartCoroutine("ChangeToDeathScene");
                count++;
            }
            else
            {
                return;
            }
            
            
        }
        
        
        
    }

    IEnumerator ChangeToGameScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator ChangeToDeathScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("DeathScene");
    }
}
