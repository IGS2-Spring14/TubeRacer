using UnityEngine;
using System.Collections;

public class DebrisSpawner : MonoBehaviour 
{
	public int SpawnIntensity = 10;
	public int SpawnMinTime = 1000;
	public int SpawnMaxTime = 2000;
	public Rigidbody[] Debris;

	private float timer = 0;

	// Use this for initialization
	void Start () 
	{
		timer = Random.Range(SpawnMinTime, SpawnMaxTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime * 100f;

		if (timer < 0)
		{
			timer = Random.Range(SpawnMinTime, SpawnMaxTime);
			Rigidbody debris = Debris[Random.Range(0, 2)];
			Vector3 offset = new Vector3(Random.Range(0, transform.localScale.x * 10), 0,
			                             Random.Range (0, transform.localScale.z * 10));

			Quaternion rot = new Quaternion();
			rot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 270f, transform.rotation.eulerAngles.z);
			Rigidbody clone;
			clone = Instantiate (debris, transform.position + offset, rot) as Rigidbody;
		}
	}
}
