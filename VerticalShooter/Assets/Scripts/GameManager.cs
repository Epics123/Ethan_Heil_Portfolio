using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public CameraShake camShake;
    public AudioSource enemyDeathSound;
    public AudioSource playerDeathSound;
    public GameObject player;
    public bool playerDead = false;
    public int playerLives = 3;

    bool playSound = true;

    // Start is called before the first frame update
    void Start()
    {
        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlayer();
    }

    public void LooseLives()
    {
        playerLives--;
    }

    void DestroyPlayer()
    {
        if(playerLives <= 0)
        {
            playerDead = true;
            if(playSound)
            {
                StartCoroutine(PlayDeathSound());
                Instantiate(player.GetComponent<PlayerController>().explosion, player.transform.position, player.transform.rotation);
                camShake.power = 0.7f;
                camShake.shouldShake = true;
                playSound = false;
            }      
            
            
            Destroy(player);
        }
    }

    IEnumerator PlayDeathSound()
    {
        playerDeathSound.Play();
        yield return null;
    }
}
