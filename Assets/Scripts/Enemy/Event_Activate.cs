using UnityEngine;
using System.Collections;

public class Event_Activate : MonoBehaviour {

	public GameObject ShipToActivate; 
	bool IsTriggered = false; 
	New_EnemyControllerWithFollow MyController;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider myCollider)
	{
		
		Debug.Log (myCollider.gameObject.name);
		if (myCollider.gameObject.name == "PlayerShip" && !IsTriggered)
		{
			Debug.Log ("Event Triggered");
			this.IsTriggered = true;
		//	MyController = ShipToActivate.GetComponent<EnemyControllerWithFollow> ();
			ShipToActivate.SetActive(true);
		}
	}
}
