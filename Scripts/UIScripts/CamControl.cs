using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {

    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
	public Camera currentCamera;
	
	void Start () 
    {
        DiagonalCamView();
	}
    public void TopCamView()
    {
        cam2.enabled = false;
        cam1.enabled = false;
        cam3.enabled = true;

		currentCamera = cam3;
    }
    public void SideCamView()
    {
        cam3.enabled = false;
        cam1.enabled = false;
        cam2.enabled = true;

		currentCamera = cam2;
    }
    public void DiagonalCamView()
    {
        cam3.enabled = false;
        cam2.enabled = false;
        cam1.enabled = true;

		currentCamera = cam1;
    }

        
	
	}

