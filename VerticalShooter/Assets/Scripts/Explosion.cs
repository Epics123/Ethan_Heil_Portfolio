using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedDestroyExplosion(delay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayedDestroyExplosion(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
