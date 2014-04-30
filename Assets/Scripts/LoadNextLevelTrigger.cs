using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class LoadNextLevelTrigger : MonoBehaviour 
{
	public string SceneName;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter (Collider col) 
	{
		if (col.gameObject.tag == "PlayerShip")
		{
			Application.LoadLevel (SceneName);
		}
	}
}
