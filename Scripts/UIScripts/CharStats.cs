using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharStats : MonoBehaviour {
    public GameManager Gamemanager;
    public Text HealthText;
    public Text MovementNUM;

	void Start () 
    {
        //Gamemanager.selectedUnit.GetComponent<Unit>().totalHealt;
	    
	}

    void Update()
    {
		if(Gamemanager.selectedUnit != null){
        	HealthText.text = "hp: " + Gamemanager.selectedUnit.GetComponent<Unit>().totalHealt;
        	MovementNUM.text = "m: " + Gamemanager.selectedUnit.GetComponent<Unit>().moveRange;
		}
		else{
			HealthText.text = null;
			MovementNUM.text = null;
		}
    }
	

}
