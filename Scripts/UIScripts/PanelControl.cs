using UnityEngine;
using System.Collections;

public class PanelControl : MonoBehaviour {

    private bool isContained = true;
    
	void Start () 
    {
        print(transform.position);
        //this.transform.position = new Vector3(159f, -179f, 0);
       this.transform.position = new Vector3(340f, -340f, 0);
	}

    public void Contain()
    {
        if (isContained == true)
        {
            //this.transform.position = new Vector3(159f, 105f, 0);
           this.transform.position = new Vector3(340f, 105f, 0);
            isContained = false;
        }
        else if (isContained == false)
        {
            //this.transform.position = new Vector3(159f, -179f, 0);
           this.transform.position = new Vector3(340f, -340f, 0);

            isContained = true;
        }

        
          
    }
}
