using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to control the behavior of the player
public class PlayerController : MonoBehaviour
{

    public GameManager gm;
    public GameObject explosion;
    public CameraShake camShake;
    public AudioSource hitSound;
    public AudioSource itemSound;
    public Image red;
    public Color lerpedColor;
    public Color startColor;
    public float flashTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        startColor = new Color(1, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Checks to see if the player hit something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player hit by enemy laser
        if(collision.gameObject.tag == "EnemyLaser")
        {
            hitSound.Play();
            gm.LooseLives();
            camShake.power = 0.2f;
            camShake.shouldShake = true;
            StartCoroutine(FlashRed(0.5f));
            Destroy(collision.gameObject);
        }
        //Player hit by enemy
        if (collision.gameObject.tag == "Enemy")
        {
            hitSound.Play();
            gm.LooseLives();
            camShake.power = 0.4f;
            camShake.shouldShake = true;
            StartCoroutine(FlashRed(0.5f));
        }
        //Player hits powerup
        if(collision.gameObject.tag == "ShootMode")
        {
            itemSound.Play();
        }
    }

    //Flashes a red image when hit
    IEnumerator FlashRed(float time)
    {
        float currentTime = 0f;
        lerpedColor = new Color(1, 0, 0, 0.5f);
        Color currentColor;
        do
        {
            currentColor = Color.Lerp(startColor, lerpedColor, Mathf.PingPong(Time.time, 0.5f));
            red.color = currentColor;
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime < time);

        red.color = startColor;
    }
}
