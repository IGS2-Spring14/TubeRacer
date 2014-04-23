using UnityEngine;
using System.Collections;

public class BossTurretController : MonoBehaviour 
{
	//Aiming
	public float MaxGunAngle = 60f;
	public Transform target, GunTransform;
	
	//Firing
	public GameObject projectile;
	public int FiringCooldown = 1000;
	public int StaggerTime = 0;
	float timer = 0;
	
	// Both
	public SplineInterpolator TheirSpline;
	public int Range = 500; 
	public float LeadTime = 10;
	public int MinRange = 0; 
	//Vector3 relPos;
	
	Boss_1_AI MyAI;
	
	//Temp Destroy
	//public float Destroy_Timer = 5;
	//private float Destroy_Time;
	
	// Use this for initialization
	void Awake ()
	{
		target = GameObject.Find ("PlayerShip").transform;
		
		GameObject targetObject = GameObject.Find ("GamePlatform");
		TheirSpline = targetObject.GetComponent<SplineInterpolator> ();
		
		//GameObject MyBoss = GameObject.Find("Prototype_Boss_Prefab(Clone)");
		//MyAI = MyBoss.GetComponent<Boss_1_AI> ();
		this.GetComponent<Boss_1_AI> ();
	}
	
	void Start () 
	{
		//target = GameObject.FindGameObjectWithTag("Player").transform;
		//GunTransform = transform.FindChild("Firing_Gun").transform;
		timer = StaggerTime;
		//Destroy_Time = Time.time + (Destroy_Timer * 3);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//UpdateDestroy ();
		//Debug.Log (Destroy_Time);
		
		if (Vector3.Distance(target.position, transform.position) > MinRange)
			if (Vector3.Distance(target.position, transform.position) < Range) 
		{
			UpdateAimRotation ();
			UpdateFiring ();
		}
	}
	
	void UpdateAimRotation()
	{
		//Direction to look at (needs to be reversed so model faces player)
		//Vector3 relPos = target.position - transform.position;
		Vector3 relPos = TheirSpline.GetHermiteAtTime (TheirSpline.mCurrentTime + (LeadTime * TheirSpline.TimeScale)) - transform.position;
		relPos.y = 0.0f;
		
		
		//Face the turret toward the player (y is axis of rotation)
		transform.rotation = Quaternion.LookRotation(relPos);
		
		
		//Face the gun toward the player
		//relPos = target.position - transform.position;
		relPos = TheirSpline.GetHermiteAtTime (TheirSpline.mCurrentTime + (LeadTime * TheirSpline.TimeScale)) - transform.position;
		if (Vector3.Angle(relPos, transform.forward) <= MaxGunAngle)
			GunTransform.rotation = Quaternion.LookRotation(relPos);
		
		//Debug info
		//print(Vector3.Angle(relPos, transform.forward));
		//Debug.DrawLine(GunTransform.position, target.position, Color.red);
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
			timer = FiringCooldown;
			GameObject clone;
			clone = Instantiate (projectile, transform.position, GunTransform.rotation) as GameObject;
		}
	}
	
	/*void UpdateDestroy ()
	{
		if (Time.time > Destroy_Time)
		{
			MyAI.SendMessage ("TurretDeath");
			Destroy (this.gameObject);
		}
	}*/
	
	
}