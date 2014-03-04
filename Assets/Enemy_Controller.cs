using UnityEngine;
using System.Collections;

public class Enemy_Controller : MonoBehaviour 
{
	public Transform target;
	public Rigidbody projectile;
	public int FiringCooldown = 1000;
	float timer = 0;
	public int Range = 500; 
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance(target.position, transform.position) < Range) 
		{
			UpdateAimRotation ();
			UpdateFiring ();
		}
	}

	void UpdateAimRotation()
	{
		//Direction to look at (needs to be reversed so model faces player)
		Vector3 relPos = target.position - transform.position;
		
		//Face the turret toward the player (y is axis of rotation)
		transform.rotation = Quaternion.LookRotation(relPos);
		

	}

	// Update is called once per frame
	void UpdateFiring () 
	{
		if (timer > 1)
		{
			timer -= (Time.deltaTime * 1000);
		}
		if (timer <= 1)
		{
			timer = FiringCooldown;
			Rigidbody clone;
			clone = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody;
		}
	}
}
