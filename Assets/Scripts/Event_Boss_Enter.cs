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

	void OnTriggerEnter(Collider myCollider)
	{
		
		Debug.Log (myCollider.gameObject.name);
		if (myCollider.gameObject.name == "GamePlatform" && !IsTriggered)
		{
			Debug.Log ("Event Triggered");
			IsTriggered = true;
			
			Vector3 Temp = transform.position;
			Temp.x = 1000;
			Temp.y = 0;
			Temp.z = 0;
			
			GameObject Boss;
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
			Boss = Instantiate (BossObject, Temp, transform.rotation) as GameObject;  
		}
	}
}
