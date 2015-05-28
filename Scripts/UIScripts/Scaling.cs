using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scaling : MonoBehaviour {
    public GameManager gamemanagerScript;
    

    void Start()
    {
        
    }
    void Update()
    {
        Image image = GetComponent<Image>();
		image.fillAmount = ((float)gamemanagerScript.playerUnits[0].baseHealt / (float)gamemanagerScript.playerUnits[0].baseHealt) - ((float)gamemanagerScript.playerUnits[0].totalDamage / 10);
        
        //print(image.fillAmount);
       

       
    }
}
