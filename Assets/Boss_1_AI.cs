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
	int Health = 4; 

	void Awake ()
	{
		target = GameObject.Find ("PlayerShip").transform;
		targetObject = GameObject.Find ("GamePlatform");
		
		SplineController MySplineControl = this.GetComponent<SplineController> ();
		MySplineControl.SplineRoot = GameObject.Find ("Path_Boss");
	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(target.position, transform.position) > FollowDistance && !isFollowing)
		{
			UpdateFollowing(); 
		}

		if (Health == 0)
		{
			BossDeath();
		}
	}

	void UpdateFollowing()
	{
		TheirSpline = targetObject.GetComponent<SplineInterpolator> ();
		MySpline = this.GetComponent<SplineInterpolator> ();

		MySpline.TimeScale = TheirSpline.TimeScale;
		isFollowing = true; 
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
}
