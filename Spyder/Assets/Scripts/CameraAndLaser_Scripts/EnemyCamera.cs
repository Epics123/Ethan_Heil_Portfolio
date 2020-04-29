using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCamera : MonoBehaviour
{
    // * Variables *
    SecurityManager security;

    CameraDetect cmra;
    Image indicator;

    SpriteRenderer vision;

	private CameraSound soundSource;

    public string linkCode; // This MUST be the same as all linked lasers;

    public Color detectColor;
    public Color indic;

    public float maxTime; // this is roughly in seconds. Determines how long the player has to be spotted before the lasers turn on.
    float time;

    bool playerDetected = false;
    bool lasersOn = false;

    // ** Update Functions **
    private void Awake()
    {
        security = GameObject.Find("SecurityManager").GetComponent<SecurityManager>();

        indicator = transform.Find("IndicatorCanvas").Find("Indicator").GetComponent<Image>();
        indicator.fillAmount = 0;
        indicator.color = indic;
    }

    private void Start()
    {
        cmra = transform.Find("Vision").GetComponent<CameraDetect>();
        vision = cmra.gameObject.GetComponent<SpriteRenderer>();
		soundSource = GetComponent<CameraSound>();

        security.SetLaserColor(linkCode, indic);
    }


    private void Update()
    {
        CheckForPlayer();
    }


    // **** Other Functions ****
    void CheckForPlayer()
    {
        if (playerDetected == false)
        {
            if (cmra.seesPlayer) // This increases when the player is in cameras vision, and decreases when not.
            {
                if (time / maxTime < 1)
                {
                    time += Time.deltaTime;
                    indicator.fillAmount = time / maxTime;
                }
				soundSource.playShutter();
            }
            else
            {
                if (time / maxTime > 0)
                {
                    time -= Time.deltaTime;
                    indicator.fillAmount = time / maxTime;
                }
            }

            if (time / maxTime >= 1)
            {
                playerDetected = true;
            }
        }

        if (playerDetected && !lasersOn)
        {
            SendSignal();

            vision.color = detectColor;

			if (cmra.seesPlayer)
				soundSource.playShutter();
		}
	}


    void SendSignal()
    {
        security.Trigger(linkCode);
    }

}
