using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scaling : MonoBehaviour {
    public GameManager gamemanagerScript;
	public int playerNumber;
    

    void Start()
    {
        
    }
    void Update()
    {
        Image image = GetComponent<Image>();
        image.fillAmount = (float)gamemanagerScript.playerUnits[playerNumber].totalHealt / (float)gamemanagerScript.playerUnits[playerNumber].baseHealt;
    }
}
