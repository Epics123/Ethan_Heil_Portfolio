using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider cooldownBar;
    public Color lerpedColor;
    public Color startColor;
    public Image background;
    public Image fill;

	// Use this for initialization
	void Start () {
        cooldownBar.value = 0.0f;
        //startColor = fill.color;
        startColor = new Color(0f, 0.725f, 0.169f, 1);
        cooldownBar.gameObject.layer = gameObject.layer;
	}
	
	// Update is called once per frame
	void Update () {
        cooldownBar.gameObject.layer = gameObject.layer;
    }

    public IEnumerator increaseValue(float time)
    {
        fill.enabled = true;
        fill.color = startColor;
        background.enabled = true;
        cooldownBar.value = 0.0f;
        Color lerpColor = Color.black;
        float currentTime = 0.0f;
        do
        {
            cooldownBar.value = Mathf.Lerp(0.0f, 1.0f, currentTime / time);
            lerpColor = Color.Lerp(startColor, Color.white, Mathf.PingPong(Time.time, 0.5f));
            fill.color = lerpColor;
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime < time);
        cooldownBar.value = 1.0f;
        StartCoroutine(hideChargeBar());

    }

    public IEnumerator hideChargeBar()
    {
        float currentTime = 0.0f;
        do
        {
            lerpedColor = Color.Lerp(fill.color, Color.white, Mathf.PingPong(Time.time, 0.25f));
            fill.color = lerpedColor;
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime < 0.30f);

        //yield return new WaitForSeconds(0.15f);
        fill.enabled = false;
        background.enabled = false;
    }

}
