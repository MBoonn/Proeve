using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharStats : MonoBehaviour {
    public GameManager Gamemanager;
    public Text CharType;
    public Text MovementNUM;
    public Canvas Fire;
    public Canvas Water;
    public Canvas Earth;
    public Canvas Air;
    
    void Start()
    {
        Water.enabled = false;
        Air.enabled = false;
        Earth.enabled = false;
        Fire.enabled = false;
    }
    void FixedUpdate()
    {
		if(Gamemanager.selectedUnit != null){
    
        	MovementNUM.text = "m: " + Gamemanager.selectedUnit.GetComponent<Unit>().moveRange;
            //CharType.text = Gamemanager.selectedUnit.GetComponent<Unit>().unitElement.ToString();
            
            switch (Gamemanager.selectedUnit.GetComponent<Unit>().unitElement)
            {
                case Unit.UnitElements.fire:
                    Water.enabled = false;
                    Air.enabled = false;
                    Earth.enabled = false;
                    Fire.enabled = true;
                    break;
                    
                case Unit.UnitElements.water:
                    Fire.enabled = false;
                    Air.enabled = false;
                    Earth.enabled = false;
                    Water.enabled = true;
                    break;

                case Unit.UnitElements.air:
                    Water.enabled = false;
                    Fire.enabled = false;
                    Earth.enabled = false;
                    Air.enabled = true;
                    break;
                case Unit.UnitElements.earth:
                    Water.enabled = false;
                    Air.enabled = false;
                    Fire.enabled = false;
                    Earth.enabled = true;
                    break;
                    default:
                    Water.enabled = false;
                    Air.enabled = false;
                    Earth.enabled = false;
                    Fire.enabled = false;
                    break;

            }

            
            
		}
		else{
			MovementNUM.text = null;
           
		}

        
    }
	

}
