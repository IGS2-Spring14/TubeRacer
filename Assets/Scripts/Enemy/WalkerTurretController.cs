using UnityEngine;
using System.Collections;

public class WalkerTurretController : MonoBehaviour 
{
	//Aiming
	public float MaxGunAngle = 60f;
	public Transform target, GunTransform;
	
	//Firing
	public GameObject projectile;
	public int FiringCooldown = 1000;
	public int StaggerTime = 0;
	float timer = 0;
	public AudioSource firingSFX;
	public Transform MainGun, LeftRack, RightRack; 

	// Both
	public SplineInterpolator Spline;
	public int Range = 500; 
	public float LeadTime = 10;
	public int MinRange = 0; 
	Vector3 relPos;
	
	// Use this for initialization
	void Awake ()
	{
		target = GameObject.Find ("PlayerShip").transform;
		
		GameObject targetObject = GameObject.Find ("GamePlatform");
		Spline = targetObject.GetComponent<SplineInterpolator> ();
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
		{
			UpdateAimRotation ();
			UpdateFiring ();
		}
	}
	
	void UpdateAimRotation()
	{
		//Direction to look at (needs to be reversed so model faces player)
		relPos = Spline.GetHermiteAtTime (Spline.mCurrentTime + (LeadTime * Spline.TimeScale)) - transform.position;
		relPos.y = 0.0f;
		
		//Face the turret toward the player (y is axis of rotation)
		transform.rotation = Quaternion.LookRotation(relPos);
		
		//Face the gun toward the player
		relPos = Spline.GetHermiteAtTime (Spline.mCurrentTime + (LeadTime * Spline.TimeScale)) - transform.position;
		if (Vector3.Angle(relPos, transform.forward) <= MaxGunAngle)
			GunTransform.rotation = Quaternion.LookRotation(relPos);

		// Rotations for side guns
		//Quaternion.Angle (relPos, MainGun.transform); 

		//Debug info
		//print(Vector3.Angle(relPos, transform.forward));
		//Debug.DrawLine(GunTransform.position, target.position, Color.red);

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
			clone = Instantiate (projectile, MainGun.position, GunTransform.rotation) as GameObject;
			clone = Instantiate (projectile, LeftRack.position, GunTransform.rotation) as GameObject;
			clone = Instantiate (projectile, RightRack.position, GunTransform.rotation) as GameObject;
			//if (!firingSFX.isPlaying)
			firingSFX.Play ();
			//print (firingSFX.isPlaying);
		}
	}
}
