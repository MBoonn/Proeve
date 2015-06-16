using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {

    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
	public Camera currentCamera;
    public int camNum = 1;
	
	void Start () 
    {
        camNum = 1;
        currentCamera = cam1;
        CamSwitch();
	}
    public void numInc()
    {
        if (camNum < 4)
        {
            camNum += 1;
            if (camNum >= 4)
            {
                camNum = 1;
            }
            
        }
        CamSwitch();
    }
    public void CamSwitch()
    {
        if (camNum == 1)
        {
            cam3.enabled = false;
            cam2.enabled = false;
            cam1.enabled = true;

            currentCamera = cam1;
        }
        else if (camNum == 2)
        {
            cam3.enabled = false;
            cam1.enabled = false;
            cam2.enabled = true;

            currentCamera = cam2;
        }
        else if (camNum == 3)
        {
            cam2.enabled = false;
            cam1.enabled = false;
            cam3.enabled = true;

            currentCamera = cam3;
        }
    }
	}

