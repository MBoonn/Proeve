using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public enum UnitTypes{type1,type2, type3, type4};
	public enum UnitElements{air, water, earth, fire};
	public enum UnitFactions{playerUnit, enemyUnit};

	public UnitTypes unitType;
	public UnitElements unitElement;
	public UnitFactions unitFaction;

	public GameManager gameManager;
	public Grid currentGrid;

	public int baseHealt;
	public float totalHealt;
	public float elementDamageMultiplier;
	public float positionDamageMultiplier = 1;
	public int baseAttack;
	public float totalAttack;
	public Unit attackTarget;
	public bool isInAttackRange = false;
	public int moveRange;

	public bool enemyUnitIsDone;
	public bool hasMoved = false;
	public bool hasAttacked = false;

    public Transform target;
    public float movementSpeed = 3;
    Vector3[] path;
    int targetIndex;
	public bool pathFollowing = false;
	public Node nodeUnitIsStandingOn;
	public GameObject selectCrystal;
	public Animation unitModelAnimation;

	public void Start(){
		setUnitType();
		totalHealt = baseHealt;
		nodeUnitIsStandingOn = GetNodeFromUnitPosition(transform.position);
	}

    public void UnitUpdate()
    {
		if(!hasMoved && gameObject.activeSelf){
			if( path == null && !(transform.position.x == target.position.x && transform.position.y == target.position.y)){
				StopCoroutine("FollowPath");
	        	PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
			}
			else if (!pathFollowing){
				StartCoroutine("FollowPath");
			}
			else if ((transform.position.x == target.position.x && transform.position.y == target.position.y) || enemyUnitIsDone){
				if(unitFaction == UnitFactions.playerUnit)
					gameManager.DeselectUnit();
				StopCoroutine("FollowPath");
				pathFollowing = false;
				hasMoved = true;
				nodeUnitIsStandingOn = GetNodeFromUnitPosition(transform.position);
				enemyUnitIsDone = false;
				path = null;
				unitModelAnimation.Play("idl");
			}
		}
	}

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
		path = newPath;
    }

    IEnumerator FollowPath()
    {
		if(path != null)
		{
	    	Vector3 currentWaypoint = path[0];
			Vector3 fixedWaypointPosition = new Vector3(currentWaypoint.x, currentWaypoint.y + transform.position.y, currentWaypoint.z);
			pathFollowing = true;
	        while (true)
	        {
	            if (transform.position == fixedWaypointPosition)
	            {
	                targetIndex++;
	                if (targetIndex >= moveRange || targetIndex >= path.Length)
	                {
						if(this.tag == "Unit Friendly")
							target.position = fixedWaypointPosition;
						else
							enemyUnitIsDone = true;
						path = null;
						targetIndex = 0;
	                    yield break;
	                }

	                currentWaypoint = path[targetIndex];
					fixedWaypointPosition = new Vector3(currentWaypoint.x, currentWaypoint.y + transform.position.y, currentWaypoint.z);
	            }
				unitModelAnimation.Play("walk");
	            transform.position = Vector3.MoveTowards(transform.position,fixedWaypointPosition,movementSpeed * Time.deltaTime);
				yield return null;
	        }
		}
    }

	public Node GetNodeFromUnitPosition(Vector3 _unitPosition){
		Node tempNode;
		tempNode = currentGrid.NodeFromWorldPoint(_unitPosition);
		return tempNode;
	}

	public void setUnitType()
	{
		switch(unitType)
		{
		case UnitTypes.type1:
			baseHealt = 4;
			baseAttack = 2;
			moveRange = 1;
			break;
		case UnitTypes.type2:
			baseHealt = 6;
			baseAttack = 3;
			moveRange = 4;
			break;
		case UnitTypes.type3:
			baseHealt = 10;
			baseAttack = 5;
			moveRange = 2;
			break;
		case UnitTypes.type4:
			baseHealt = 8;
			baseAttack = 4;
			moveRange = 3;
			break;
		}
	}

	public float DamageCalculation(Unit _SelectedUnit, UnitElements _SelectedUnitElement, UnitElements _enemyUnitElement){
		switch(_SelectedUnitElement)
		{
		case UnitElements.air:
			if(_enemyUnitElement == UnitElements.air)
				_SelectedUnit.elementDamageMultiplier = 1;
			if(_enemyUnitElement == UnitElements.earth)
				_SelectedUnit.elementDamageMultiplier = 0.5f;
			if(_enemyUnitElement == UnitElements.water)
				_SelectedUnit.elementDamageMultiplier = 1.5f;
			if(_enemyUnitElement == UnitElements.fire)
				elementDamageMultiplier = 0.5f;
				break;
		case UnitElements.earth:
			if(_enemyUnitElement == UnitElements.air)
				_SelectedUnit.elementDamageMultiplier = 0.5f;
			if(_enemyUnitElement == UnitElements.earth)
				_SelectedUnit.elementDamageMultiplier = 1;
			if(_enemyUnitElement == UnitElements.water)
				_SelectedUnit.elementDamageMultiplier = 0.5f;
			if(_enemyUnitElement == UnitElements.fire)
				_SelectedUnit.elementDamageMultiplier = 1;
			break;
		case UnitElements.fire:
			if(_enemyUnitElement == UnitElements.air)
				_SelectedUnit.elementDamageMultiplier = 1.5f;
			if(_enemyUnitElement == UnitElements.earth)
				_SelectedUnit.elementDamageMultiplier = 0.5f;
			if(_enemyUnitElement == UnitElements.water)
				_SelectedUnit.elementDamageMultiplier = 0.5f;
			if(_enemyUnitElement == UnitElements.fire)
				_SelectedUnit.elementDamageMultiplier = 1;
			break;
		case UnitElements.water:
			if(_enemyUnitElement == UnitElements.air)
				_SelectedUnit.elementDamageMultiplier = 0.5f;
			if(_enemyUnitElement == UnitElements.earth)
				_SelectedUnit.elementDamageMultiplier = 1;
			if(_enemyUnitElement == UnitElements.water)
				_SelectedUnit.elementDamageMultiplier = 1;
			if(_enemyUnitElement == UnitElements.fire)
				_SelectedUnit.elementDamageMultiplier = 1.5f;
			break;
		}

		switch(nodeUnitIsStandingOn.nodeType)
		{
		case 0:
			_SelectedUnit.positionDamageMultiplier = 1;
			break;
		case 1:
			if(_SelectedUnitElement == UnitElements.fire)
				_SelectedUnit.positionDamageMultiplier = 1.5f;
			else
				_SelectedUnit.positionDamageMultiplier = 1;
			break;
		case 2:
			if(_SelectedUnitElement == UnitElements.water)
				_SelectedUnit.positionDamageMultiplier = 1.5f;
			else
				_SelectedUnit.positionDamageMultiplier = 1;
			break;
		case 3:
			if(_SelectedUnitElement == UnitElements.earth)
				_SelectedUnit.positionDamageMultiplier = 1.5f;
			else
				_SelectedUnit.positionDamageMultiplier = 1;
			break;
		case 4:
			if(_SelectedUnitElement == UnitElements.air)
				_SelectedUnit.positionDamageMultiplier = 1.5f;
			else
				_SelectedUnit.positionDamageMultiplier = 1;
			break;
		}
		float tempDamage = _SelectedUnit.baseAttack * _SelectedUnit.positionDamageMultiplier;
		return tempDamage * _SelectedUnit.elementDamageMultiplier;
	}

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
