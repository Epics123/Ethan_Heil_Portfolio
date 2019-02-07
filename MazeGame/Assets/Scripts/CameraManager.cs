using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls which level is active and being rendered

public class CameraManager : MonoBehaviour
{

    //Enum to control the current level the player is on
    public enum LevelState
    {
        LEVEL1,
        LEVEL2,
        LEVEL3
    }

    public Camera hiddenCamera;
    public Camera activeCamera;
    public GameObject lvl1Grid;
    public GameObject lvl2Grid;
    public GameObject lvl1Buttons;
    public GameObject lvl2Buttons;
    public GameManager gm;
    public TVShader tvShader;
    public LevelState level;

    float contrast;
    float brightness;

    // Start is called before the first frame update
    void Start()
    {
        hiddenCamera.enabled = false;
        lvl2Grid.SetActive(false);
        tvShader = activeCamera.GetComponent<TVShader>();
        contrast = tvShader.contrast;
        brightness = tvShader.brightness;
    }

    //Swaps the current camera to change levels
    public void SwapCameras()
    {
        StartCoroutine(SwapEffectStart(0.5f));
        var swapCam = hiddenCamera;

        gm.levelIdx++; //Increment level

        switch (gm.levelIdx)
        {
            //Sets everything with the 'level2' layer to active
            case 2: level = LevelState.LEVEL2;
                gm.player.layer = gm.lvl2Layer;
                gm.floor.layer = gm.lvl2Layer;
                lvl2Grid.SetActive(true);
                lvl1Grid.SetActive(false);
                lvl2Buttons.SetActive(true);
                lvl1Buttons.SetActive(false);
                break;
            //Activates win state
            case 3:
                level = LevelState.LEVEL3;
                gm.player.layer = gm.lvl3Layer;
                gm.floor.layer = gm.lvl3Layer;
                gm.win = true;
                break;
            //Sets everything with the 'level1' layer to active
            default:
                level = LevelState.LEVEL1;
                gm.player.layer = gm.startLayer;
                gm.floor.layer = gm.startLayer;
                lvl2Grid.SetActive(false);
                lvl1Grid.SetActive(true);
                lvl2Buttons.SetActive(false);
                lvl1Buttons.SetActive(true);
                gm.levelIdx = 1;
                break;
        }

        //Swap cameras
        hiddenCamera = activeCamera;
        hiddenCamera.enabled = false;
        activeCamera = swapCam;
        activeCamera.enabled = true;
        tvShader = activeCamera.GetComponent<TVShader>();
        StartCoroutine(SwapEffectEnd(0.5f));
    }

    //Starts the swap effect
    IEnumerator SwapEffectStart(float time)
    {
        float currentTime = 0.0f;

        //lerps the contrast and brightness of the crt shader so screen colors are inverted
        do
        {
            tvShader.contrast = Mathf.Lerp(contrast, 20f, currentTime / time);
            tvShader.brightness = Mathf.Lerp(brightness, 200f, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);


        yield return new WaitForSeconds(1.0f);
    }

    //Ends the swap effect
    IEnumerator SwapEffectEnd(float time)
    {
        float currentTime = 0.0f;

        //lerps the contrast and brightness of the crt shader so screen colors are inverted
        do
        {
            tvShader.contrast = Mathf.Lerp(20f, contrast, currentTime / time);
            tvShader.brightness = Mathf.Lerp(200f, brightness, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        //reset contrast and brightness
        tvShader.contrast = 12.2f;
        tvShader.brightness = 0f;
        yield return new WaitForSeconds(1.0f);
    }
}
