﻿using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour 
{
	public int Projectile_Speed = 500;
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
}