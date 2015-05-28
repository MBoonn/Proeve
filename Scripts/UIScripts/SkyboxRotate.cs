using UnityEngine;
using System.Collections;

public class SkyboxRotate : MonoBehaviour {

	public float speed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles += new Vector3 (0,speed * Time.deltaTime,0);
	}
}
