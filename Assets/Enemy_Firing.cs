using UnityEngine;
using System.Collections;

public class Enemy_Firing : MonoBehaviour {

	// Firing
	public GameObject projectile;
	public int FiringCooldown = 1000;
	public int Range = 500;
	float timer = 0;
	public Transform target;
	public GameObject targetObject;
	private SplineInterpolator TheirSpline;
	public float LeadTime = 0; 

	void Awake()
	{
		target = GameObject.Find ("PlayerShip").transform;
		targetObject = GameObject.Find ("GamePlatform");
		TheirSpline = targetObject.GetComponent<SplineInterpolator> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(target.position, transform.position) < Range) 
		{
			UpdateAimRotation ();
			UpdateFiring ();
		}
	}

	void UpdateAimRotation()
	{
		//Direction to look at (needs to be reversed so model faces player)
		//Vector3 relPos = target.position - transform.position;
		Vector3 relPos = TheirSpline.GetHermiteAtTime (TheirSpline.mCurrentTime + (LeadTime * TheirSpline.TimeScale)) - transform.position;
		relPos.y -= 1000; 
		//Debug.Log ("relPos: " + relPos.y);
		//Debug.Log ("target: " + target.transform.position.y);

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
}
