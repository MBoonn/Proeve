using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public enum GameStates{bootUp, playing, paused, ended}
	public enum Turns{playersTurn, enemysTurn}

	public CamControl cameraControleScript;
	//public ScreenManger screenManger;
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
	
	public void Awake () {
	
	}

	public void Update () {
		if(currentTurn == Turns.playersTurn){
			if (Input.GetMouseButtonDown(0))
			{
				if(selectedUnit !=null){
					SetTargetForPlayerUnit();
					SetAttackTargetForPlayerUnit(selectedUnit.GetComponent<Unit>());
				}
				else{
					SelectUnit();
				}
			}
				

			if(Input.GetKeyDown(KeyCode.A) && selectedUnit.GetComponent<Unit>().isInAttackRange && !selectedUnit.GetComponent<Unit>().hasAttacked){
				Attack(selectedUnit.GetComponent<Unit>(), attackTarget.GetComponent<Unit>());
			}/**/

			if(selectedUnit != null && targetToMoveTo != null)
			{
				selectedUnit.GetComponent<Unit>().UnitUpdate();
			}
		}
		else if(currentTurn == Turns.enemysTurn){
			for(int i = 0; i< enemyUnits.Length;i++)
			{
				if(enemyUnits[i].hasMoved == false)
				{
					if(enemyTargetToMoveTo == null){
						for(int j = 0; j<playerUnits.Length;j++){
							if(playerUnits[j].gameObject.activeSelf){
								enemyUnits[i].isInAttackRange = CheckInRange(enemyUnits[i].transform, playerUnits[j].transform);
								if(enemyUnits[i].isInAttackRange && !enemyUnits[i].hasAttacked)
									Attack(enemyUnits[i], playerUnits[j]);
							}
						}
						enemyTargetToMoveTo = SetTargetForEnemyUnit(enemysGoal);
						enemyUnits[i].target = enemyTargetToMoveTo as Transform;
						enemyUnits[i].UnitUpdate();
					}
					else{
						enemyUnits[i].target = enemyTargetToMoveTo as Transform;
						enemyUnits[i].UnitUpdate();
					}
				}
				else if(enemyUnits[i].pathFollowing && !enemyUnits[i].hasMoved){
					enemyUnits[i].UnitUpdate();
				}
				 else if(enemyUnits[i].unitType == Unit.UnitTypes.type2 && enemyUnits[i].pathFollowing == false && enemyUnits[i].hasMoved){
					enemysTurnIsDone = true;
				}
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

	public void SelectUnit(){
		Ray ray;
		RaycastHit hit;
		ray = cameraControleScript.currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			print (hit.collider.name);
			if(hit.collider.tag == "Unit Friendly"){
				selectedUnit = hit.collider.gameObject.transform;
				selectedUnit.GetComponent<Unit>().selectCrystal.SetActive(true);
			}
		}
	}

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
		}
		/*Ray ray;
		RaycastHit hit;
		ray = cameraControleScript.currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			print (hit.collider.name);
			if(hit.collider.tag == "Unit Friendly")
				selectedUnit = hit.collider.gameObject.transform;
		}/**/
	}

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

	public Object SetTargetForEnemyUnit(Transform _enemysGoal)
	{
		Object tempTarget;
		tempTarget = Instantiate(targetPrefab, _enemysGoal.position, Quaternion.identity) as Object;
		return tempTarget;
	}

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

	public bool CheckInRange(Transform _selectedUnit, Transform _attackTarget){
		if((_selectedUnit.position-_attackTarget.position).magnitude < 4){
			print ("in range");
			return true;
		}
		else{
			print ("not in range");
			return false;
		}
	}

	public void Attack(Unit _selectedUnit, Unit _attackTarget){
		_selectedUnit.unitModelAnimation.Play("attack");
		_attackTarget.unitModelAnimation.Play("hit");
		_selectedUnit.totalAttack = _selectedUnit.DamageCalculation(_selectedUnit, _selectedUnit.unitElement, _attackTarget.unitElement);
		print (_selectedUnit.totalAttack);
		_attackTarget.totalHealt = _attackTarget.totalHealt - _selectedUnit.totalAttack;
		_selectedUnit.hasAttacked = true;
		_attackTarget.selectCrystal.SetActive(false);
		if(_selectedUnit.unitModelAnimation.isPlaying){
			_selectedUnit.unitModelAnimation.Play("idl");
			_attackTarget.unitModelAnimation.Play("idl");
		}
		_selectedUnit.attackTarget = null;
		LifeCheck();
	}

	public void LifeCheck(){
		for(int i = 0;i<playerUnits.Length;i++)
		{
			if(playerUnits[i].totalHealt <=0){
				playerUnits[i].gameObject.SetActive(false);
				playerUnits[i].unitModelAnimation.Play("lose");
			}
		}

		for(int i = 0;i<enemyUnits.Length;i++)
		{
			if(enemyUnits[i].totalHealt <=0){
				enemyUnits[i].gameObject.SetActive(false);
				enemyUnits[i].unitModelAnimation.Play("lose");
			}
		}
	}

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
				print("Enemys Turn!");
				currentTurn = Turns.enemysTurn;
				break;

			case Turns.enemysTurn:
				foreach(Unit u in playerUnits)
                {
					u.hasMoved = false;
				    u.hasAttacked = false;
				}
				print("Players Turn!");
				DeselectUnit();
				currentTurn = Turns.playersTurn;
				break;
		}
 	}

    public void PTOverload()
    {
        PassOnTurn(currentTurn);
    }
	
    public void AttackFunction()
    {
      if (selectedUnit.GetComponent<Unit>().isInAttackRange && !selectedUnit.GetComponent<Unit>().hasAttacked)
      {
        Attack(selectedUnit.GetComponent<Unit>(), attackTarget.GetComponent<Unit>());
      }
    }
	public void SwitchGameState(){

	}

	public GameStates CheckGameState(){
		return currentState;
	}
}
