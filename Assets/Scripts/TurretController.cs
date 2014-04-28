using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour 
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
	public int BulletOffsetRange = 0; 

	// Both
	public SplineInterpolator Spline;
	public int Range = 500; 
	public float LeadTime = 10;
	public int MinRange = 0; 
	//Vector3 relPos;

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
		Vector3 relPos = Spline.GetHermiteAtTime (Spline.mCurrentTime + (LeadTime * Spline.TimeScale)) - transform.position;
		relPos.y = 0.0f;


		//Face the turret toward the player (y is axis of rotation)
		transform.rotation = Quaternion.LookRotation(relPos);


		//Face the gun toward the player
		relPos = Spline.GetHermiteAtTime (Spline.mCurrentTime + (LeadTime * Spline.TimeScale)) - transform.position;
		if (Vector3.Angle(relPos, transform.forward) <= MaxGunAngle)
			GunTransform.rotation = Quaternion.LookRotation(relPos);

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

			// randomize the bullet offset
			Vector3 TempPos = transform.position;
			TempPos.x += Random.Range(-BulletOffsetRange, BulletOffsetRange);
			TempPos.y += Random.Range(-BulletOffsetRange, BulletOffsetRange);
			TempPos.z += Random.Range(-BulletOffsetRange, BulletOffsetRange);
			
			// create the bullet
			clone = Instantiate (projectile, TempPos, GunTransform.rotation) as GameObject;

			//if (!firingSFX.isPlaying)
			firingSFX.Play ();
			//print (firingSFX.isPlaying);
		}
	}
}