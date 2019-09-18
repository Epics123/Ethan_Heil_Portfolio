using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEffects : AbstractBehavior
{

    public GameObject portal;
    public CameraManager camera;
    public UIManager uiManager;
    Vector3 originalScale;
    float tempgravity;
    bool pressInput = true;
    public float portalCooldownTimer = 5;
    public float portalCooldown = 5;
    public float infectedPortalCooldown = 3;


    // Use this for initialization
    void Start ()
    {
        infectedPortalCooldown = portalCooldown;
        portal.GetComponent<MeshRenderer>().enabled = false;
        originalScale = Vector3.zero;
        if (this.GetComponent<TaggingManager>().tagged)
        {
            StartCoroutine(uiManager.increaseValue(portalCooldown));
        }
        else
        {
            StartCoroutine(uiManager.increaseValue(portalCooldown));
        }   
    }
	
	// Update is called once per frame
	void Update ()
    {
        portalCooldownTimer += Time.deltaTime;
        var portal = inputState.GetButtonValue(inputButtons[0]);

        if (portal && pressInput && (portalCooldownTimer > portalCooldown || (this.GetComponent<TaggingManager>().tagged && portalCooldownTimer > infectedPortalCooldown)))
        {
            ActivatePortal();
        }
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //   ActivatePortal();
        //}
    }

    void ActivatePortal()
    {
        portal.GetComponent<MeshRenderer>().enabled = true;
        pressInput = false;
        //tempgravity = body2D.gravityScale;
        //body2D.gravityScale = 0;
        //body2D.velocity = Vector2.zero;
        //this.GetComponent<CircleCollider2D>().enabled = false;
        //ToggleScripts(false);
        ExpandPortal();
    }

    void DeactivatePortal()
    {
        portal.transform.localScale = new Vector3(41f, 41f, 1f);
        portal.GetComponent<MeshRenderer>().enabled = false;
        //this.GetComponent<CircleCollider2D>().enabled = true;
        //body2D.gravityScale = tempgravity;
        portalCooldownTimer = 0;
        //ToggleScripts(true);
        pressInput = true;
    }

    void ExpandPortal()
    {
        StartCoroutine(ScaleUpOverTime(0.5f));
    }

    IEnumerator ScaleUpOverTime(float time)
    {
        Vector3 destinationScale = new Vector3(80f, 80f, 1f);

        float currentTime = 0.0f;

        do
        {
            portal.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        yield return new WaitForSeconds(.25f);
        if (this.GetComponent<TaggingManager>().tagged)
        {
            StartCoroutine(uiManager.increaseValue(infectedPortalCooldown));
        }
        else
        {
            StartCoroutine(uiManager.increaseValue(portalCooldown));
        }
        camera.ChangeCamera();
        StartCoroutine(ScaleDownOverTime(0.5f));
       
    }

    IEnumerator ScaleDownOverTime(float time)
    {
        Vector3 currentScale = portal.transform.localScale;

        float currentTime = 0.0f;

        do
        {
            portal.transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        DeactivatePortal();
    }
}
