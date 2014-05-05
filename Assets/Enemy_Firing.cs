using UnityEngine;
using System.Collections;

public class Enemy_Firing : MonoBehaviour {

	// Firing
	public GameObject projectile;
	public int FiringCooldown = 1000;
	public int Range = 500;
	float timer = 0;
	private Transform TargetShip;
	public GameObject TargetPlatform;
	private SplineInterpolator TheirSplineInterpolator;
	public float Lead = 1; 
	public int BulletOffsetRange = 0; 
	private float DistanceAdjust = 1000; 
	//private float LeadAdjust = 1.0; 

	void Awake()
	{
		TargetShip = GameObject.Find ("PlayerShip").transform;
		TargetPlatform = GameObject.Find ("GamePlatform");
		TheirSplineInterpolator = TargetPlatform.GetComponent<SplineInterpolator> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(TargetShip.position, transform.position) < Range) 
		{
			UpdateAimRotation ();
			UpdateFiring ();
		}
	}

	void UpdateAimRotation()
	{
		//Direction to look at (needs to be reversed so model faces player)
		Vector3 relPos = TargetShip.position - transform.position;
		float distance = Vector3.Distance (this.transform.position, TargetShip.transform.position);
		//Debug.Log ("My forward: " + TargetShip.transform.forward);
		//Debug.Log ("My distance: " + distance);

		//Debug.Log ("My adjust: " + DistanceAdjust); 

		Vector3 offset = TargetShip.transform.forward * TheirSplineInterpolator.TimeScale * Lead * (distance / DistanceAdjust); 
		//Vector3 relPos = TheirSpline.GetHermiteAtTime (TheirSpline.mCurrentTime + (LeadTime * TheirSpline.TimeScale)) - transform.position;
		//relPos.y -= 1000; 
		//Debug.Log ("relPos: " + relPos.y);
		//Debug.Log ("target: " + target.transform.position.y);

		//Face the turret toward the player (y is axis of rotation)
		//Debug.Log ("And then: " + offset);
		transform.rotation = Quaternion.LookRotation(relPos + offset);
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
		}
	}
}
