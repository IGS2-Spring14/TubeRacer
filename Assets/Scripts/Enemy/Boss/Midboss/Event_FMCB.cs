using UnityEngine;
using System.Collections;

public class Event_FMCB : MonoBehaviour {
	
	public GameObject MidbossObject;
	bool IsActive = false;
	/*
	public enum ePathDirection { X, Y, Z };
	public ePathDirection mPathDirection = ePathDirection.X; 
	*/
	GameObject targetObject;
	SplineInterpolator TheirSplineInterpolator; 

	void Awake()
	{
		targetObject = GameObject.Find ("GamePlatform");
		TheirSplineInterpolator = targetObject.GetComponent<SplineInterpolator> ();	
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter(Collider myCollider)
	{
		
		//Debug.Log (myCollider.gameObject.name);
		if (myCollider.gameObject.name == "PlayerShip" && !IsActive)
		{
			Debug.Log ("Event Triggered");
			IsActive = true;

			Vector3 Temp = transform.position;
			/*
			if (mPathDirection == ePathDirection.X)
			{
				Temp.x += 1000;
				Temp.y = 0;
				Temp.z = 0;
			}
			else if (mPathDirection == ePathDirection.Y)
			{
				Temp.x = 0;
				Temp.y += 1000;
				Temp.z = 0;
			}
			else
			{
				Temp.x = 0;
				Temp.y = 0;
				Temp.z += 1000;
			}*/

			GameObject Boss;
			Boss = Instantiate (MidbossObject, Temp, transform.rotation) as GameObject;  

			FMCB_Activate();
		}
	}

	void FMCB_Activate()
	{
		Debug.Log ("Oh no, a FMCB!");
		TheirSplineInterpolator.StopShip = true;  
	}

	void FMCB_Destroyed()
	{
		Debug.Log ("The FMCB has been deactivated!");
		TheirSplineInterpolator.ResetSpeed = true;
	}
}
