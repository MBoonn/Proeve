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

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY , Transform _nodePrefab)
    {
		nodePrefab = _nodePrefab;

        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
		if(walkable){
			node = Transform.Instantiate(nodePrefab,worldPosition,Quaternion.identity) as GameObject;
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
}
