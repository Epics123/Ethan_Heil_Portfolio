using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject miniExplosion;
    public float speed = 10f;
    public float laserDamage = 50f;
    public int lifeSpan = 5;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyLaser")
        {
            Instantiate(miniExplosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        //if(gameObject.tag == "EnemyLaser" && )
    }

}
