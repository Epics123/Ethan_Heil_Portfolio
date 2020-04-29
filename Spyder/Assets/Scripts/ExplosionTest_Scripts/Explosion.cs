using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // * Variables *
    public float spawnSize = 0.25f;
    public float expansionMultiplier = 3f;
    public float expandTime = 1f; // in seconds

    float currentSize = 0f;
    float currentTime = 0f;

    SpriteRenderer spr;


    // ** Update Functions **
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        currentSize = spawnSize;
    }

    private void Update()
    {
        transform.localScale = new Vector3 (currentSize, currentSize, 1);

        Expand();
    }

    // **** Other Functions ****
    void Expand()
    {
        currentTime += Time.deltaTime;
        currentSize += (Time.deltaTime * expansionMultiplier);

        if (currentTime >= expandTime)
        {
            Destroy(gameObject);
        }
    }

}
