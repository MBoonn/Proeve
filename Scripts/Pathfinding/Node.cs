using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {
	public enum NodeStates{normal,path};

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

	public Object nodePrefab;
	public GameObject node;
	public NodeStates nodeState;
	public int nodeType;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY , Transform _nodePrefab,int _nodeType)
    {
		nodePrefab = _nodePrefab;

        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
		nodeType = _nodeType;
		if(walkable){
			ChangeNodeType(nodeType);
		}
    }

    public int fcost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fcost.CompareTo(nodeToCompare.fcost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

	public void ChangeNodeType(int _nodeType){
		switch(_nodeType){
		case 0:
			node = Transform.Instantiate(nodePrefab,worldPosition,Quaternion.identity) as GameObject;
			break;
		case 1:
			node = Transform.Instantiate(Resources.Load("Tile_Fire"),worldPosition,Quaternion.identity) as GameObject;
			break;
		case 2:
			node = Transform.Instantiate(Resources.Load("Tile_Water"),worldPosition,Quaternion.identity) as GameObject;
			break;
		case 3:
			node = Transform.Instantiate(Resources.Load("Tile_Earth"),worldPosition,Quaternion.identity) as GameObject;
			break;
		case 4:
			node = Transform.Instantiate(Resources.Load("Tile_Air"),worldPosition,Quaternion.identity) as GameObject;
			break;
		}
	}
}
