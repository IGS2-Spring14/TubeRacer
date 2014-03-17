using UnityEngine;
using System.Collections;

public class Event_Boss_Enter : MonoBehaviour {

	public GameObject BossObject;
	bool IsTriggered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision myCollision)
	{

		Debug.Log (myCollision.gameObject.name);
		if (myCollision.gameObject.name == "GamePlatform" && !IsTriggered)
		{
			Debug.Log ("Event Triggered");
			IsTriggered = true;

			Vector3 Temp = transform.position;
			Temp.x = 0;
			Temp.y = 1000;
			Temp.z = 2000;

			GameObject Boss;
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject; 
		}
	}
}
