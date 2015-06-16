using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public enum GameStates{bootUp, playing, won, lose, paused, ended}
	public enum Turns{playersTurn, enemysTurn}

	public CamControl cameraControleScript;
	public Unit[] playerUnits;
	public Unit[] enemyUnits;

	public Transform selectedUnit;
	public Transform targetPrefab;
	public Object targetToMoveTo;
	public Transform attackTarget;
	public Transform enemysGoal;
	public Object enemyTargetToMoveTo;
	public bool enemysTurnIsDone;

	public GameStates currentState;
	public Turns currentTurn;

    public Canvas Fire;
    public Canvas Water;
    public Canvas Earth;
    public Canvas Air;
    public Canvas LoseScreen;
	
	public void Start () {
        LoseScreen.enabled = false;
	}

	public void Update () {
		//hier word er gekeken wiens beurt het is.
		//als het de speler zijn buert is dan worden if statements gecheckt en uitgevoerd
		if(currentTurn == Turns.playersTurn){
            if (currentState == GameStates.won)
            {

            }
            else if (currentState == GameStates.lose)
            {
                LoseScreen.enabled = true;
            }
                

			if (Input.GetMouseButtonDown(0))
			{
				if(selectedUnit !=null){
					SetTargetForPlayerUnit();
					SetAttackTargetForPlayerUnit(selectedUnit.GetComponent<Unit>());
				}
				else{
					SelectUnit();
				}

                Ray ray;
		        RaycastHit hit;
		        ray = cameraControleScript.currentCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Deselect")
                    {
                        DeselectUnit();
                    }
                }
			}

			if(selectedUnit != null && targetToMoveTo != null)
			{
				selectedUnit.GetComponent<Unit>().UnitUpdate();
			}
		}

		//als het de enemys beurt is dan worden deze if statements geheckt en uitgevoerd
		else if(currentTurn == Turns.enemysTurn){
            foreach (Unit e in enemyUnits)
            {
                if (e.hasMoved == false)
                {
                    if (enemyTargetToMoveTo == null)
                    {
                        for (int j = 0; j < playerUnits.Length; j++)
                        {
                            if (playerUnits[j].gameObject.activeSelf)
                            {
                                e.isInAttackRange = CheckInRange(e.transform, playerUnits[j].transform);
                                if (e.isInAttackRange && !e.hasAttacked)
                                    Attack(e, playerUnits[j]);
                            }
                        }
                        enemyTargetToMoveTo = SetTargetForEnemyUnit(enemysGoal);
                        e.target = enemyTargetToMoveTo as Transform;
                        e.UnitUpdate();
                    }
                    else
                    {
                        e.target = enemyTargetToMoveTo as Transform;
                        e.UnitUpdate();
                        if (e.unitType == Unit.UnitTypes.type4 && e.hasMoved)
                            enemysTurnIsDone = true;
                    }
                }
				if (e.atGoal)
					currentState = GameStates.lose;
			}

			if(enemysTurnIsDone){
				enemyTargetToMoveTo = null;
				for(int i =0;i<enemyUnits.Length;i++)
				{
					for(int j = 0; j<playerUnits.Length;j++){
						if(!enemyUnits[i].isInAttackRange)
							enemyUnits[i].isInAttackRange = CheckInRange(enemyUnits[i].transform, playerUnits[j].transform);
						if(enemyUnits[i].isInAttackRange && !enemyUnits[i].hasAttacked)
							Attack(enemyUnits[i], playerUnits[j]);
					}
					enemyUnits[i].target = null;
				}
				PassOnTurn(currentTurn);
			}
		}
	}

	//in deze function word het selecteren van de unit geregeld
	public void SelectUnit(){
		Ray ray;
		RaycastHit hit;
		ray = cameraControleScript.currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if(hit.collider.tag == "Unit Friendly"){
				selectedUnit = hit.collider.gameObject.transform;
				selectedUnit.GetComponent<Unit>().selectCrystal.SetActive(true);
			}
		}
	}

	//in deze function word het deselecteren van de units geregeld
	public void DeselectUnit()
	{
		if(selectedUnit){
			selectedUnit.GetComponent<Unit>().unitModelAnimation.Play("idl");
			selectedUnit.GetComponent<Unit>().selectCrystal.SetActive(false);
			if(attackTarget){
				attackTarget.GetComponent<Unit>().selectCrystal.SetActive(false);
				attackTarget = null;
			}
			selectedUnit = null;
			targetToMoveTo = null;

            Water.enabled = false;
            Air.enabled = false;
            Earth.enabled = false;
            Fire.enabled = false;
		}
	}

	//als deze function word aangeroepen zorgt deze er voor dat de speler unit een target krijgt om naar toe te lopen
	public void SetTargetForPlayerUnit()
	{
		Ray ray;
		RaycastHit hit;
		ray = cameraControleScript.currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if(hit.collider.tag == "Ground")
			{
				if(targetToMoveTo == null){
					targetToMoveTo = Instantiate(targetPrefab, hit.point, Quaternion.identity) as Object;
					selectedUnit.GetComponent<Unit>().target = targetToMoveTo as Transform;
				}
				else{
					Destroy((targetToMoveTo as Transform).gameObject);
					targetToMoveTo = Instantiate(targetPrefab, hit.point, Quaternion.identity) as Object;
					selectedUnit.GetComponent<Unit>().target = targetToMoveTo as Transform;
				}
			}
		}
	}
	
	//als deze function word aangeroepen zorgt deze er voor dat de enemy unit een target krijgt om naar toe te lopen
	public Object SetTargetForEnemyUnit(Transform _enemysGoal)
	{
		Object tempTarget;
		tempTarget = Instantiate(targetPrefab, _enemysGoal.position, Quaternion.identity) as Object;
		return tempTarget;
	}

	//in deze krijgt het geselecteerde unit een target om aan te vallen
	public void SetAttackTargetForPlayerUnit(Unit selectedPlayerUnit){
		Ray ray;
		RaycastHit hit;
		ray = cameraControleScript.currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if(hit.collider.tag == "Unit Enemy"){
				selectedPlayerUnit.attackTarget = hit.collider.GetComponent<Unit>();
				attackTarget = hit.collider.transform;
				attackTarget.GetComponent<Unit>().selectCrystal.SetActive(true);
				selectedPlayerUnit.isInAttackRange = CheckInRange(selectedUnit.transform, hit.collider.transform);
			}
		}
	}

	//hier word gekeken of de geselecteerde unit en zijn target niet meer dan 3 blokken van elkaar afstaan
	public bool CheckInRange(Transform _selectedUnit, Transform _attackTarget){
		if((_selectedUnit.position-_attackTarget.position).magnitude < 4){
			return true;
		}
		else{
			return false;
		}
	}

	//in deze function word de hele attack fase geregeld
	public void Attack(Unit _selectedUnit, Unit _attackTarget){
		_selectedUnit.totalAttack = _selectedUnit.DamageCalculation(_selectedUnit, _selectedUnit.unitElement, _attackTarget.unitElement);
		_attackTarget.totalHealt = _attackTarget.totalHealt - _selectedUnit.totalAttack;
		_selectedUnit.hasAttacked = true;
		_attackTarget.selectCrystal.SetActive(false);
		_selectedUnit.attackTarget = null;
		LifeCheck();
	}

	//hier word gekeken de units niet onder de 0 levens punten te zijn 
	public void LifeCheck(){
		for(int i = 0;i<playerUnits.Length;i++)
		{
			if(playerUnits[i].totalHealt <=0){
				playerUnits[i].gameObject.SetActive(false);
				//playerUnits[i].unitModelAnimation.Play("lose");
			}
            
		}

		for(int i = 0;i<enemyUnits.Length;i++)
		{
			if(enemyUnits[i].totalHealt <=0){
				enemyUnits[i].gameObject.SetActive(false);
				//enemyUnits[i].unitModelAnimation.Play("lose");
			}
		}

        if (playerUnits.Length <= 0)
            currentState = GameStates.lose;
        if (enemyUnits.Length <= 0)
            currentState = GameStates.won;
	}

	//hier word de beurt door gegeven aan de desbetrevende persoon
    public void PassOnTurn(Turns _currentTurn)
    {
		switch(_currentTurn)
        {
			case Turns.playersTurn:
				enemysTurnIsDone = false;
				foreach(Unit u in enemyUnits)
                {
					u.hasMoved = false;
					u.hasAttacked = false;
				}
				currentTurn = Turns.enemysTurn;
				break;

			case Turns.enemysTurn:
				foreach(Unit u in playerUnits)
                {
					u.hasMoved = false;
				    u.hasAttacked = false;
				}
                foreach (Unit u in enemyUnits)
                {
                    u.hasMoved = false;
                    u.hasAttacked = false;
                }
				DeselectUnit();
				currentTurn = Turns.playersTurn;
				break;
		}
 	}

	/// <summary>
	/// deze function hebben allemaal iets te maken met de ui
	/// </summary>

    public void PTOverload()
    {
        PassOnTurn(currentTurn);
    }
    public void AttackFunction()
    {
        if (selectedUnit != null)
        {
            if (selectedUnit.GetComponent<Unit>().isInAttackRange && !selectedUnit.GetComponent<Unit>().hasAttacked)
            {
                Attack(selectedUnit.GetComponent<Unit>(), attackTarget.GetComponent<Unit>());
            }
        }
    }
	public void SwitchGameState(){

	}

	public GameStates CheckGameState(){
		return currentState;
	}
}
