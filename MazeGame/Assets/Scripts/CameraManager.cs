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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwapCameras();
        }
    }

    public void SwapCameras()
    {
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
    }
}
