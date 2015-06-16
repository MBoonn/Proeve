using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelControl : MonoBehaviour {

    private bool isContained = false;
    private bool MenuConfirmDisplay = false;
    public Canvas Panel;
    public Canvas MenuConfirm;
    
    
	void Start () 
    {
        if (MenuConfirm != null)
        {
            MenuConfirm.enabled = false;
        }
        
	}

    public void Contain()
    {
        if (isContained == true)
        {

            Panel.enabled = true;
            isContained = false;
        }
        else if (isContained == false)
        {

            Panel.enabled = false;

            isContained = true;
        }

        
          
    }


    public void MenuConfirmFunction()
    {
        if (MenuConfirm != null)
        { 
            if (MenuConfirmDisplay == false)
            {
                MenuConfirm.enabled = true;
                MenuConfirmDisplay = true;

            }
            else if (MenuConfirmDisplay == true)
            {
                MenuConfirm.enabled = false;
                MenuConfirmDisplay = false;
            }
        }
    }
}
