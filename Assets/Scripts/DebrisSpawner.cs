using UnityEngine;
using System.Collections;

public class DebrisSpawner : MonoBehaviour 
{
	public int SpawnIntensity = 10;
	public int SpawnMinTime = 500;
	public int SpawnMaxTime = 1200;
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
		timer -= Time.deltaTime * 1000f;

		if (timer < 0)
		{
			timer = Random.Range(SpawnMinTime, SpawnMaxTime);

            for (int i = 0; i < SpawnIntensity; i++)
            {
                Rigidbody debris = Debris[Random.Range(0, Debris.Length-1)];
                Vector3 offset = new Vector3(Random.Range(0, transform.localScale.x * 10), 0, Random.Range(0, transform.localScale.z * 10));

                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 270f, transform.rotation.eulerAngles.z);
                Rigidbody clone;
                clone = Instantiate(debris, transform.position + offset, rot) as Rigidbody;
                Vector3 scale = clone.gameObject.transform.localScale;
                clone.gameObject.transform.localScale = scale * Random.Range(20.0f, 60.0f);
            }
		}
	}
}
