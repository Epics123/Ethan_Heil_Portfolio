using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

    public Color color;
    public float rate;

    Color lerpedColor;

    // Use this for initialization
    void Start()
    {
        lerpedColor = GetComponent<Text>().color;

    }

    // Update is called once per frame
    void Update()
    {
        lerpedColor = Color.Lerp(color, Color.black, Mathf.PingPong(Time.time, rate));
        GetComponent<Text>().color = lerpedColor;
    }
}
