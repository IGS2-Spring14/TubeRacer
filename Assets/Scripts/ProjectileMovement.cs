using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour 
{
	public int Projectile_Speed = 1000;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 vSpeedDelta = transform.forward * (Time.deltaTime * Projectile_Speed);
		transform.position += (vSpeedDelta);
	}
	void onCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		   	{
				Debug.Log("hit player");
			}
	}
}