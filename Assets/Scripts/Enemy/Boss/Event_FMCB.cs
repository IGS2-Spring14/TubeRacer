using UnityEngine;
using System.Collections;

public class Event_FMCB : MonoBehaviour {
	
	public GameObject MidbossObject;
	bool IsActive = false; 
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
		
		Debug.Log (myCollider.gameObject.name);
		if (myCollider.gameObject.name == "GamePlatform" && !IsActive)
		{
			Debug.Log ("Event Triggered");
			IsActive = true;
			
			Vector3 Temp = transform.position;
			Temp.x += 1000;
			Temp.y = 0;
			Temp.z = 0;
			
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
