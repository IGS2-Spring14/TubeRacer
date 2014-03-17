using UnityEngine;
using System.Collections;

public class Enemy_Controller : MonoBehaviour 
{
	public Transform target;
	public GameObject targetObject;
	public Rigidbody projectile;
	public int FiringCooldown = 1000;
	float timer = 0;
	public int Range = 500; 
	public int FollowDistance = 5000; 
	internal bool isFollowing = false;
	SplineInterpolator TheirSpline;
	SplineInterpolator MySpline;

	void Awake ()
	{
		target = GameObject.Find ("PlayerShip").transform;
		targetObject = GameObject.Find ("GamePlatform");

		SplineController TheirSplineControl = target.GetComponent<SplineController> ();
		SplineController MySplineControl = this.GetComponent<SplineController> ();
		MySplineControl.SplineRoot = GameObject.Find ("Path");
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

		if (Vector3.Distance(target.position, transform.position) > FollowDistance && !isFollowing)
		{
			UpdateFollowing(); 
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

	void UpdateFollowing()
	{
		TheirSpline = targetObject.GetComponent<SplineInterpolator> ();
		MySpline = this.GetComponent<SplineInterpolator> ();
		
		MySpline.TimeScale = TheirSpline.TimeScale;
		isFollowing = true; 
	}
}