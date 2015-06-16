using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scaling : MonoBehaviour {
    public GameManager gamemanagerScript;
	public int playerNumber;
    
    void Update()
    {
        Image image = GetComponent<Image>();
        if (gamemanagerScript.selectedUnit!= null)
        {
            image.fillAmount = (float)gamemanagerScript.selectedUnit.GetComponent<Unit>().totalHealt / (float)gamemanagerScript.selectedUnit.GetComponent<Unit>().baseHealt;
        }
        
    }
}
