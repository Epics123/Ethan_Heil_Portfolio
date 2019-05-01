using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public ParticleSystem image, imageFade, logoParticle, logoSwirl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            image.Stop();
            imageFade.Play();
            logoParticle.Play();
            logoSwirl.Play();
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadSceneAsync("Tutorial");
    }
}
