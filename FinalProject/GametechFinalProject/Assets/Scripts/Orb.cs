using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public GameObject collisionCheck;
    public Vector3 maxScale = new Vector3(5, 5, 1);
    public Material worldLook;
    public bool thrown = false;
    public float speed = 2f;
    public float duration = 2f;

    Vector3 minScale = new Vector3 (1, 1, 1);
    bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 8);
        Physics2D.IgnoreLayerCollision(12, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (thrown == true)
        {
            Physics2D.IgnoreLayerCollision(12, 10);
            StartCoroutine(CheckSpeed());
        }
        if(stopped == true)
        {
            StartCoroutine(DeactivateOrb());
        }
    }

    IEnumerator CheckSpeed()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            stopped = true;
            GetComponent<SpriteRenderer>().material = worldLook;
            Physics2D.IgnoreLayerCollision(12, 11);
            Physics2D.IgnoreLayerCollision(12, 10, false);

            yield return ExpandOrb(minScale, maxScale, duration);
            thrown = false;
        }
    }

    IEnumerator ExpandOrb(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    IEnumerator DeactivateOrb()
    {
        yield return new WaitForSeconds(12);
        gameObject.SetActive(false);
    }

}
