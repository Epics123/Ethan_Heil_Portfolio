using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera hiddenCamera;
    public Camera activeCamera;
    public GameObject lvl1Grid;
    public GameObject lvl2Grid;
    public GameObject lvl1Buttons;
    public GameObject lvl2Buttons;
    public GameManager gm;
    public TVShader tvShader;

    float contrast;
    float brightness;

    public enum LevelState
    {
        LEVEL1,
        LEVEL2,
        LEVEL3
    }

    public LevelState level;

    // Start is called before the first frame update
    void Start()
    {
        hiddenCamera.enabled = false;
        lvl2Grid.SetActive(false);
        tvShader = activeCamera.GetComponent<TVShader>();
        contrast = tvShader.contrast;
        brightness = tvShader.brightness;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwapCameras()
    {
        StartCoroutine(SwapEffectStart(0.5f));
        var swapCam = hiddenCamera;

        gm.levelIdx++;

        switch (gm.levelIdx)
        {
            case 2: level = LevelState.LEVEL2;
                gm.player.layer = gm.lvl2Layer;
                gm.floor.layer = gm.lvl2Layer;
                lvl2Grid.SetActive(true);
                lvl1Grid.SetActive(false);
                lvl2Buttons.SetActive(true);
                lvl1Buttons.SetActive(false);
                break;
            case 3:
                level = LevelState.LEVEL3;
                gm.player.layer = gm.lvl3Layer;
                gm.floor.layer = gm.lvl3Layer;
                gm.win = true;
                break;
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

        hiddenCamera = activeCamera;
        hiddenCamera.enabled = false;
        activeCamera = swapCam;
        activeCamera.enabled = true;
        tvShader = activeCamera.GetComponent<TVShader>();
        StartCoroutine(SwapEffectEnd(0.5f));
    }

    IEnumerator SwapEffectStart(float time)
    {
        float currentTime = 0.0f;

        do
        {
            tvShader.contrast = Mathf.Lerp(contrast, 20f, currentTime / time);
            tvShader.brightness = Mathf.Lerp(brightness, 200f, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);


        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator SwapEffectEnd(float time)
    {
        float currentTime = 0.0f;

        do
        {
            tvShader.contrast = Mathf.Lerp(20f, contrast, currentTime / time);
            tvShader.brightness = Mathf.Lerp(200f, brightness, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        tvShader.contrast = 12.2f;
        tvShader.brightness = 0f;
        yield return new WaitForSeconds(1.0f);
    }
}
