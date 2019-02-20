using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform cam;

    public float power = 0.01f;
    public float duration = 0.01f;
    public float slowDownAmount = 0.01f;
    public bool shouldShake = false;

    Vector3 startPos;
    float initalDuration;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        startPos = cam.localPosition;
        initalDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldShake)
        {
            if(duration > 0)
            {
                cam.localPosition = startPos + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initalDuration;
                cam.localPosition = startPos;
            }
        }
    }

}
