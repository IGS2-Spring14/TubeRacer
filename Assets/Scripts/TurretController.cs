using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour 
{
	//public float TurningRate = 100f;
	public float MaxGunAngle = 60f;

	Transform target, xform;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		xform = transform.FindChild("Firing_Gun").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateAimRotation();
	}

	void UpdateAimRotation()
	{
		//Direction to look at (needs to be reversed so model faces player)
		Vector3 relPos = target.position - transform.position;
		relPos = relPos * -1;
		relPos.y = 0.0f;

		//Face the turret toward the player (y is axis of rotation)
		transform.rotation = Quaternion.LookRotation(relPos);

		//Face the gun toward the player
		relPos = target.position - transform.position;
		relPos = relPos * -1;
		if (Vector3.Angle(relPos, transform.forward) <= MaxGunAngle)
			xform.rotation = Quaternion.LookRotation(relPos);

		//Debug info
		print(Vector3.Angle(relPos, transform.forward));
		Debug.DrawLine(xform.position, target.position, Color.red);
	}
}
