using UnityEngine;
using System.Collections;

public class Midboss_AI : MonoBehaviour 
{
	
	public Transform target;
	public GameObject targetObject;
	public int FollowDistance = 8000;

	public enum ePathDirection { X, Y, Z };
	public ePathDirection mPathDirection = ePathDirection.X; 

	internal bool isFollowing = false;
	SplineInterpolator TheirSplineInterpolator;
	SplineInterpolator MySpline;
	public int Health = 4; 
	Event_FMCB MyEventScript;
	GameObject MyEventObject;
	public GameObject PathPrefab;

	GameObject myPath;
	
	void Awake ()
	{
		target = GameObject.Find ("PlayerShip").transform;
		targetObject = GameObject.Find ("GamePlatform");
		
		SplineController MySplineControl = this.GetComponent<SplineController> ();
		//myPath = GameObject.Find ("Path_Midboss");
		//MySplineControl.SplineRoot = myPath;

		MyEventObject = GameObject.Find ("Event_FMCB"); 
		MyEventScript = MyEventObject.GetComponent<Event_FMCB> (); 

		// my path
		// path rotation
		Quaternion TempRot = transform.rotation;
		TempRot.x = 0;
		TempRot.y = 0;
		TempRot.z = 0;

		// path position
		Vector3 TempPos = targetObject.transform.position;	
		if (mPathDirection == ePathDirection.X)
		{
			TempPos.x += 1000;
		}
		else if (mPathDirection == ePathDirection.Y)
		{
			TempPos.y += 1000;
		}
		else
		{
			TempPos.z += 1000;
		}

		GameObject clone;
		clone = Instantiate (PathPrefab, TempPos, TempRot) as GameObject;
		Debug.Log (clone.transform.position);
		MySplineControl.SplineRoot = GameObject.Find (clone.name);
	}
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if (Vector3.Distance(target.position, transform.position) > FollowDistance && !isFollowing)
		{
			isFollowing = true; 
			UpdateFollowing(); 
		}
		*/
		Vector3 relPos = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(relPos);

		if (Health == 1)
		{

		}

		if (Health == 0)
		{
			BossDeath();
		}
	}
/*
	void UpdateFollowing()
	{
		TheirSplineInterpolator = targetObject.GetComponent<SplineInterpolator> ();
		MySpline = this.GetComponent<SplineInterpolator> ();
		
		MySpline.TimeScale = TheirSplineInterpolator.TimeScale;
	}
*/	
	void ComponentDeath()
	{
		Health--;
		Debug.Log (Health);
	}
	
	void BossDeath()
	{
		Debug.Log ("Boss Defeated");
		MyEventScript.SendMessage ("FMCB_Destroyed");
		Destroy (this.gameObject);
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag ("Gun")) {
			Destroy(collision);
		}
	}

}
