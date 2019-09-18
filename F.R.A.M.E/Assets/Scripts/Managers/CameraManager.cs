using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public enum WorldLayers
    {
        WORLD_0,
        WORLD_1,
        WORLD_2
    }

    public Camera hiddenCamera;
    public Camera activeCamera;
    public Camera[] cameras;
    public TVShader tvShader;

    public GameObject player;
    public GameObject portal;

    public WorldLayers worldState;
    public int worldIdx;

    float contrast;
    float brightness;

    // Use this for initialization
    void Start ()
    {
        var rt = new RenderTexture(Screen.width, Screen.height, 24);
        Shader.SetGlobalTexture("_TimeCrackTexture", rt);
        hiddenCamera.targetTexture = rt;
        tvShader = activeCamera.GetComponent<TVShader>();
        ChangeCamera();
        //worldState = WorldLayers.WORLD_0;
        //worldIdx = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void ChangeCamera()
    {

        contrast = tvShader.contrast;
        brightness = tvShader.brightness;

        StartCoroutine(TeleportEffectStart(0.5f));
       

        activeCamera.targetTexture = hiddenCamera.targetTexture;
        hiddenCamera.targetTexture = null;

        var swapCam = hiddenCamera;

        worldIdx++;

        switch (worldIdx)
        {
            case 0: worldState = WorldLayers.WORLD_0;
                player.layer = 9;
                portal.layer = 9;
                break;
            case 1: worldState = WorldLayers.WORLD_1;
                player.layer = 10;
                portal.layer = 10;
                hiddenCamera = cameras[2];
                break;
            case 2: worldState = WorldLayers.WORLD_2;
                player.layer = 11;
                portal.layer = 11;
                hiddenCamera = cameras[0];
                break;
            default: worldState = WorldLayers.WORLD_0;
                worldIdx = 0;
                player.layer = 9;
                portal.layer = 9;
                hiddenCamera = cameras[1];
                break;
        }
        var rt = new RenderTexture(Screen.width, Screen.height, 24);
        hiddenCamera.targetTexture = rt;
        Shader.SetGlobalTexture("_TimeCrackTexture", rt);
        //hiddenCamera = activeCamera;   
        activeCamera = swapCam;
        tvShader = activeCamera.GetComponent<TVShader>();
        StartCoroutine(TeleportEffectEnd(0.5f));
    }

    IEnumerator TeleportEffectStart(float time)
    {
        //tvShader.enabled = true;
        float currentTime = 0.0f;

        do
        {
            tvShader.contrast = Mathf.Lerp(contrast, 20f, currentTime/time);
            tvShader.brightness = Mathf.Lerp(brightness, 200f, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator TeleportEffectEnd(float time)
    {

        float currentTime = 0.0f;

        do
        {
            tvShader.contrast = Mathf.Lerp(20f, contrast, currentTime / time);
            tvShader.brightness = Mathf.Lerp(200f, brightness, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        tvShader.contrast = 0f;
        tvShader.brightness = -31f;
        //tvShader.enabled = false;
        yield return new WaitForSeconds(1.0f);
    }
}
