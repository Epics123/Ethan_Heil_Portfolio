using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    public Image Black;

    private void Awake()
    {
        Black.enabled = false;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartFade()
    {
        Black.canvasRenderer.SetAlpha(0.0f);
        Black.enabled = true;
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        Black.CrossFadeAlpha(1.0f, 1.0f, false);
        yield return new WaitForSeconds(1.0f);

    }
}
