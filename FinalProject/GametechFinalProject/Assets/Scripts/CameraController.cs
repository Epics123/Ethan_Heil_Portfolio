using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera activeCam;
    public Camera hiddenCam;


    void Awake()
    {
        var rt = new RenderTexture(Screen.width, Screen.height, 24);
        Shader.SetGlobalTexture("_WorldChange", rt);
        hiddenCam.targetTexture = rt;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwapCameras()
    {
        activeCam.targetTexture = hiddenCam.targetTexture;
        hiddenCam.targetTexture = null;

        var swapCam = activeCam;
        activeCam = hiddenCam;
        hiddenCam = swapCam;
    }
}
