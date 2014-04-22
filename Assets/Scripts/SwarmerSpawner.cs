using UnityEngine;
using System.Collections;

public class SwarmerSpawner : MonoBehaviour 
{
	public int spawnAmount = 15;
	public GameObject swarmer;

    public bool spawnThroughTrigger = true;
    private bool spawned = false;

	// Use this for initialization
	void Start () 
	{
        if (!spawnThroughTrigger)
        {
            spawned = Spawn();
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    private bool Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject.Instantiate(swarmer, new Vector3(Random.Range(-100000, 100000),
                                                        Random.Range(-100000, 100000),
                                                        Random.Range(-100000, 100000)), Quaternion.identity);
        }

        return true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!spawned)
                spawned = Spawn();
        }
    }
}
