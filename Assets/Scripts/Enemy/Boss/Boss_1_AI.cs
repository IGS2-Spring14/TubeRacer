using UnityEngine;
using System.Collections;

public class Boss_1_AI : MonoBehaviour 
{

	public Transform target;
	public GameObject targetObject;
	public int FollowDistance = 8000;

	internal bool isFollowing = false;
	SplineInterpolator TheirSpline;
	SplineInterpolator MySpline;
	public int Health = 8; 

	/* --------------------------------------------
	 * --------------------------------Rotation WIP
	bool isRotated = false;
	float HorizontalMovement;
	float VerticalMovement;
	Quaternion TempRot; 
	-------------------------------------------- */
	GameObject myPath;

	void Awake ()
	{
		target = GameObject.Find ("PlayerShip").transform;
		targetObject = GameObject.Find ("GamePlatform");
		
		SplineController MySplineControl = this.GetComponent<SplineController> ();
		myPath = GameObject.Find ("Path_Boss");
		MySplineControl.SplineRoot = myPath;
	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(target.position, transform.position) > FollowDistance && !isFollowing)
		{
			isFollowing = true; 
			UpdateFollowing(); 
		}

		if (Health == 0)
		{
			BossDeath();
		}

		/* --------------------------------------------
		 * --------------------------------Rotation WIP
		if (Health == 7)
			if (!isRotated)
		{
			//	transform.rotation.z = 180; 
			/*
			    isRotated = true; 
			 	TempRot = myPath.transform.rotation;
			 	TempRot.y = 180;
				myPath.transform.rotation = TempRot; 
			*/
			/*
				for (int i = 0; i < 17; i++)
				{
					//MySpline.mNodes[i].Rot.y += 180;
				}
			*/
		/*	
			isRotated = true; 
			MySpline.IsFlipped = true; 
		-------------------------------------------- */
	}

	void UpdateFollowing()
	{
		TheirSpline = targetObject.GetComponent<SplineInterpolator> ();
		MySpline = this.GetComponent<SplineInterpolator> ();

		MySpline.TimeScale = TheirSpline.TimeScale;
	}

	void TurretDeath()
	{
		Health--;
		Debug.Log (Health);
	}

	void BossDeath()
	{
		Debug.Log ("Boss Defeated");
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag ("Gun")) {
			Destroy(collision);
		}
	}

	/* --------------------------------------------
	 * --------------------------------Rotation WIP
	void UpdateRotation
	{

		 rotation += 180 * Time.deltaTime;
		
		altitude = Mathf.Clamp(altitude, minAltitude, maxAltitude);

	

		var pos = transform.parent.up * -altitude;
		var rot = Quaternion.AngleAxis(rotation, transform.forward);
		
		pos = rot * pos;
		
		var rotAngle = Quaternion.LookRotation(transform.forward, -pos);
		
		transform.rotation = rotAngle;
		transform.position = transform.parent.position + pos;
	}
	-------------------------------------------- */
}
