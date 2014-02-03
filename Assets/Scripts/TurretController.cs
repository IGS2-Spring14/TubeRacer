using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour 
{
	//Aiming
	public float MaxGunAngle = 60f;
	public bool ReverseAim = true;
	public Transform target, GunTransform;
	
	//Firing
	public Rigidbody projectile;
	public int FiringRate = 1000;
	float timer = 0;

	// Use this for initialization
	void Start () 
	{
		//target = GameObject.FindGameObjectWithTag("Player").transform;
		//GunTransform = transform.FindChild("Firing_Gun").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateAimRotation();
		UpdateFiring();
	}

	void UpdateAimRotation()
	{
		//Direction to look at (needs to be reversed so model faces player)
		Vector3 relPos = target.position - transform.position;
		if (ReverseAim)
			relPos = relPos * -1;
		relPos.y = 0.0f;

		//Face the turret toward the player (y is axis of rotation)
		transform.rotation = Quaternion.LookRotation(relPos);

		//Face the gun toward the player
		relPos = target.position - transform.position;
		if (ReverseAim)
			relPos = relPos * -1;
		if (Vector3.Angle(relPos, transform.forward) <= MaxGunAngle)
			GunTransform.rotation = Quaternion.LookRotation(relPos);

		//Debug info
		print(Vector3.Angle(relPos, transform.forward));
		Debug.DrawLine(GunTransform.position, target.position, Color.red);
	}
	
	// Update is called once per frame
	void UpdateFiring () 
	{
		if (timer > 1)
		{
			timer -= (Time.deltaTime * 1000);
		}
		if (timer <= 1)
		{
			timer = FiringRate;
			Rigidbody clone;
			clone = Instantiate (projectile, transform.position, GunTransform.rotation) as Rigidbody;
		}
	}
}
