using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameManager gm;
    public ParticleSystem ps;
    public Color unlockedColor;
    public string nextLevel;

    bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.numKeys <= 0)
        {
            unlocked = true;
            GetComponent<SpriteRenderer>().color = unlockedColor;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && unlocked == true)
        {
            collision.GetComponent<PlayerMovement>().canMove = false;
            ps.Play();
            StartCoroutine(gm.LoadNextLevel(nextLevel));
        }
    }
}
