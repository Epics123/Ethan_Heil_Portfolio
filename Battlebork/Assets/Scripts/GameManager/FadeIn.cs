using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public Image Black;

    // Use this for initialization
    void Start()
    {
        StartFade();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartFade()
    {
        Black.canvasRenderer.SetAlpha(1.0f);
        Black.enabled = true;
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        Black.CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(2.0f);
        Black.enabled = false;

    }
}
