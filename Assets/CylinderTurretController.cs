using UnityEngine;
using System.Collections;

public class CylinderTurretController : MonoBehaviour 
{
	//Aiming
	public Transform target;
	
	//Firing
	public GameObject projectile;
	public int FiringCooldown = 1000;
	public int StaggerTime = 0;
	float timer = 0;
	public AudioSource firingSFX;

	// Both
	public int Range = 500; 
	public int MinRange = 0; 
	Midboss_AI MyAI;
	
	// Use this for initialization
	void Awake ()
	{
		target = GameObject.Find ("PlayerShip").transform;
		MyAI = transform.root.GetComponent<Midboss_AI> ();
	}
	
	void Start () 
	{
		timer = StaggerTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance(target.position, transform.position) > MinRange)
			if (Vector3.Distance(target.position, transform.position) < Range) 
				if (MyAI.Health < 6)
				{
					UpdateFiring ();
				}
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
			GameObject clone;
			clone = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
			firingSFX.Play ();
		}
	}
}
