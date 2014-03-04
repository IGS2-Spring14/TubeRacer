using UnityEngine;
using System.Collections;

public class Missile_Control : MonoBehaviour {

	public int Projectile_Speed = 500;
	public float Destroy_Timer = 10;
	private float Destroy_Time;

	// Use this for initialization
	void Start () {
		Destroy_Time = Time.time + Destroy_Timer;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement ();
		UpdateDestroy ();
	}

	void UpdateMovement () 
	{
		Vector3 vSpeedDelta = transform.forward * (Time.deltaTime * Projectile_Speed);
		transform.position += (vSpeedDelta);
	}

	void UpdateDestroy ()
	{
		if (Time.time == Destroy_Time)
		{
			Destroy (this.gameObject);
		}
	}
}
