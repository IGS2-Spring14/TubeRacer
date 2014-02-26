using UnityEngine;
using System.Collections;

public class objectMovement : MonoBehaviour {

	public float pipeRotationX = 10;
	public float pipeRotationY = 10;
	public float pipeRotationZ = 10;

	public float movSpeedX = 100;
	public float movSpeedY = 100;
	public float movSpeedZ = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//rotate object on X axis pipeRotationX degrees per second
		transform.Rotate (Vector3.right * pipeRotationX * Time.deltaTime);
		//rotate object on Y axis pipeRotationY degrees per second
		transform.Rotate (Vector3.up * pipeRotationY * Time.deltaTime);
		//rotate object on Z axis pipeRotationZ degrees per second
		transform.Rotate (Vector3.forward * pipeRotationZ * Time.deltaTime);

		//move object movSpeed distance per second
		transform.position += new Vector3(movSpeedX * Time.deltaTime, movSpeedY * Time.deltaTime, movSpeedZ * Time.deltaTime);

	}
}
