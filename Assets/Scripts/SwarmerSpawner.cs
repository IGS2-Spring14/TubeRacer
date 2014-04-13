using UnityEngine;
using System.Collections;

public class SwarmerSpawner : MonoBehaviour 
{
	public int spawnAmount = 15;
	public GameObject swarmer;

	// Use this for initialization
	void Start () 
	{ 
		for (int i = 0; i < spawnAmount; i++) 
		{
			GameObject.Instantiate(swarmer, new Vector3(Random.Range(50000, 100000),
			                                            Random.Range (50000, 100000),
			                                            Random.Range(50000, 100000)), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
