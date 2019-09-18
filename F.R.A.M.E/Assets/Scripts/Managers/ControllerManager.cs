using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        int numofControllers = 0;
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i] + " is active on the port of " + (i + 1));
            if (Input.GetJoystickNames()[i].Length > 10 && numofControllers < 4)
            {
                if (Input.GetJoystickNames()[i].Substring(0, 10).Equals("Controller"))
                {
                    Debug.Log(Input.GetJoystickNames()[i] + " is now changed on the port of " + (i + 1));
                    InputManagment[] Controllers = GameObject.Find("InputManagment").GetComponents<InputManagment>();
                    Controllers[numofControllers].inputs[0].axisName = "J" + (i + 1) + "Horizontal";
                    Controllers[numofControllers].inputs[1].axisName = "J" + (i + 1) + "Horizontal";
                    // Controllers[numofControllers].inputs[2].axisName = "J" + (i + 1) + "Run";
                    Controllers[numofControllers].inputs[2].axisName = "J" + (i + 1) + "Jump";
                    // Controllers[numofControllers].inputs[4].axisName = "J" + (i + 1) + "Vertical";
                    Controllers[numofControllers].inputs[3].axisName = "J" + (i + 1) + "Portal";
                    numofControllers += 1;
                }
            }
        }
    }
}
