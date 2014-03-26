using UnityEngine;
using System.Collections;

public class EnemyControllerWithPath : MonoBehaviour 
{
	// Firing
	public GameObject projectile;
	public int FiringCooldown = 1000;
	public int Range = 500;
	float timer = 0;
	 
	// Movement
	public int FollowDistance = 5000; 
	public float MyTimeScale = 10;
	public GameObject PathPrefab;
	GameObject TheCounterObject;
	EnemyCounter TheCounterScript;
	bool isFollowing = false; 

	// Firing and movement
	public Transform target;
	public GameObject targetObject;

	// Splines
	SplineController TheirSplineControl;
	SplineController MySplineControl;
	SplineInterpolator TheirSplineInterpolator;
	SplineInterpolator MySplineInterpolator;

	void Awake ()
	{
		TheCounterObject = GameObject.Find ("EnemyCounter");
		TheCounterScript = TheCounterObject.GetComponent<EnemyCounter> (); 

		target = GameObject.Find ("PlayerShip").transform;
		targetObject = GameObject.Find ("GamePlatform");
		
		TheirSplineControl = targetObject.GetComponent<SplineController> ();
		MySplineControl = this.GetComponent<SplineController> ();
		TheirSplineInterpolator = targetObject.GetComponent<SplineInterpolator> ();
		MySplineInterpolator = this.GetComponent<SplineInterpolator> ();

		// Instantiate and initialize the path

		Quaternion Rot = transform.rotation;
		Rot.x = 0;
		Rot.y = 0;
		Rot.z = 0;
		Vector3 Temp = transform.position;
		Temp.z = Random.Range (-1000, 1000); 

		GameObject clone;
		clone = Instantiate (PathPrefab, Temp, Rot) as GameObject;
		clone.name = clone.name + TheCounterScript.EnemyCount;
		MySplineControl.SplineRoot = GameObject.Find (clone.name);
		TheCounterScript.EnemyCount++;
	}
	
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
		
		/*
		if (Vector3.Distance(target.position, transform.position) > FollowDistance && !isFollowing)
		{
			UpdateFollowing(); 
		}
		*/
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
			GameObject clone;
			clone = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
		}
	}
	
	void UpdateFollowing()
	{	
		MySplineInterpolator.TimeScale = TheirSplineInterpolator.TimeScale;
		isFollowing = true; 
	}
}
