using UnityEngine;
using System.Collections;

public class Event_Ship_Spawner : MonoBehaviour {
	
	public GameObject ShipObject;
	public GameObject MySpawnPoint;
	public GameObject PathPrefab;
	public int NumberOfShips;
	public int SpawnRangeMin = -10;
	public int SpawnRangeMax = 10;

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

			for(int i = 0; i < NumberOfShips; i++)
			{
				Vector3 Temp = MySpawnPoint.transform.position;
				Temp.x += Random.Range(SpawnRangeMax, SpawnRangeMin);
				Temp.y += Random.Range(SpawnRangeMax, SpawnRangeMin);
				Temp.z += Random.Range(SpawnRangeMax, SpawnRangeMin);

				//Quaternion Rot = Quaternion.LookRotation();


				GameObject Clone;
				Clone = Instantiate (ShipObject, Temp, transform.rotation) as GameObject;

			}
		}
	}
}
