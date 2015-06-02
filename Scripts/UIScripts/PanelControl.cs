using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelControl : MonoBehaviour {

    private bool isContained = false;
    private bool isContained2 = false;
    private bool MenuConfirmDisplay = false;
    public Canvas Panel;
    public Canvas Panel2;
    public Canvas MenuConfirm;
    
    
	void Start () 
    {
        MenuConfirm.enabled = false;
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

    public void Contain2()
    {
        if (isContained2 == true)
        {

            Panel2.enabled = true;
            isContained2 = false;
        }
        else if (isContained2 == false)
        {

            Panel2.enabled = false;

            isContained2 = true;
        }



    }

    public void MenuConfirmFunction()
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
