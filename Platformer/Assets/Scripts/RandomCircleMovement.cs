using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToRandomPos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveToRandomPos()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = Random.insideUnitCircle * 5;
        StartCoroutine(MoveToRandomPos());
    }
}
