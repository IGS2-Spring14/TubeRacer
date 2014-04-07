using UnityEngine;
using System.Collections;

public class New_EnemyControllerWithFollow : MonoBehaviour 
{
	// Firing
	public GameObject projectile;
	public int FiringCooldown = 1000;
	public int Range = 500;
	float timer = 0;
	
	// Movement
	public int FollowDistance = 5000; 
	public float MyTimeScale = 10;
	GameObject TheCounterObject;
	EnemyCounter TheCounterScript;
	bool isFollowing = false; 
	public float PositionOffsetX = 0; 
	public float PositionOffsetY = 0; 
	public float PositionOffsetZ = 0; 
	public bool RandomizeOffset = false; 
	Vector3 OffsetTransform; 
	
	// Firing and movement
	public Transform target;
	public GameObject targetObject;
	//public bool ActivatedByTrigger = false; 
	public AudioSource firingSFX;

	// Splines
	SplineController TheirSplineControl;
	SplineController MySplineControl;
	SplineInterpolator TheirSplineInterpolator;
	SplineInterpolator MySplineInterpolator;
	
	void Awake ()
	{
		//TheCounterObject = GameObject.Find ("EnemyCounter");
		//TheCounterScript = TheCounterObject.GetComponent<EnemyCounter> (); 
		
		target = GameObject.Find ("PlayerShip").transform;
		targetObject = GameObject.Find ("GamePlatform");
		
		TheirSplineControl = targetObject.GetComponent<SplineController> ();
		MySplineControl = this.GetComponent<SplineController> ();
		TheirSplineInterpolator = targetObject.GetComponent<SplineInterpolator> ();
		MySplineInterpolator = this.GetComponent<SplineInterpolator> ();
		
		// Set the path
		MySplineControl.SplineRoot = TheirSplineControl.SplineRoot;
		MySplineInterpolator.mState = TheirSplineInterpolator.mState;
	}
	
	// Use this for initialization
	void Start () 
	{
		MySplineControl.mTransforms = TheirSplineControl.mTransforms;
		MySplineControl.Duration = TheirSplineControl.Duration; 
		MySplineControl.SendMessage ("FollowSpline"); 
		MySplineInterpolator.mCurrentTime = TheirSplineInterpolator.mCurrentTime;
		MySplineInterpolator.mCurrentIdx = TheirSplineInterpolator.mCurrentIdx;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (Vector3.Distance(target.position, transform.position)); 
		if (RandomizeOffset) 
		{
			PositionOffsetX = Random.Range(-2500, 2500);
			PositionOffsetY = Random.Range(-2500, 2500);
			PositionOffsetZ = Random.Range(-2500, 2500);
			RandomizeOffset = false;
		}
		
		OffsetTransform = transform.position;
		OffsetTransform.x += PositionOffsetX;
		OffsetTransform.y += PositionOffsetY;
		OffsetTransform.z += PositionOffsetZ;
		transform.position = OffsetTransform;


		if (Vector3.Distance(target.position, transform.position) < Range) 
		{
			UpdateAimRotation ();
			if (isFollowing)
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
			GameObject clone;
			clone = Instantiate (projectile, transform.position, transform.rotation) as GameObject;
			//if (!firingSFX.isPlaying)
			firingSFX.Play();
		}
	}
	
	void UpdateFollowing()
	{	
		MySplineInterpolator.TimeScale = TheirSplineInterpolator.TimeScale;
		isFollowing = true; 
	}	
}

