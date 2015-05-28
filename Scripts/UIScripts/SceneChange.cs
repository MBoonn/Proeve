using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {
	
	public void ChangeToScreen (int toChangeTo) 
    {
        Application.LoadLevel(toChangeTo);
	}

    public void Quit()
    {
        Application.Quit();
    }
}
