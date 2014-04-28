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
	public int BulletOffsetRange = 0; 

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

			// randomize the bullet offset
			Vector3 TempPos = transform.position;
			TempPos.x += Random.Range(-BulletOffsetRange, BulletOffsetRange);
			TempPos.y += Random.Range(-BulletOffsetRange, BulletOffsetRange);
			TempPos.z += Random.Range(-BulletOffsetRange, BulletOffsetRange);
			
			// create the bullet
			clone = Instantiate (projectile, TempPos, transform.rotation) as GameObject;

			firingSFX.Play ();
		}
	}
}
