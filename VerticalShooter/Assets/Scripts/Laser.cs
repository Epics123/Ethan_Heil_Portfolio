using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public float speed = 5f;
    public int lifeSpan = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedDestroyLaser(lifeSpan));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    IEnumerator DelayedDestroyLaser(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
