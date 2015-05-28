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

	public int baseHealt;
	public int totalHealt;
	public int totalDamage;
	public int baseAttack;
	public int totalAttack;
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

	public void Awake(){
		setUnitType();
		totalHealt = baseHealt;
	}

    public void UnitUpdate()
    {
		if(!hasMoved){
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
				enemyUnitIsDone = false;
				path = null;
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

	            transform.position = Vector3.MoveTowards(transform.position,fixedWaypointPosition,movementSpeed * Time.deltaTime);
				yield return null;
	        }
		}
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
