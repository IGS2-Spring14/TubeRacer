﻿using UnityEngine;
using System.Collections;

public class Life_Midboss_Walker : MonoBehaviour {
	public int life = 3;
	GameObject clone;
	Midboss_AI MyAI;
	public Transform explosion;
	
	// Use this for initialization
	void Awake ()
	{
		//GameObject MyBoss = GameObject.Find("Prototype_Boss_Prefab(Clone)");
		MyAI = transform.root.GetComponent<Midboss_AI> ();
		//MyAI = MyBoss.GetComponent<Boss_1_AI> ();
	}
	
	
	void Start () {
		clone = this.gameObject;
		//life = 3;
	}
	
	public void Die()
	{
		Debug.Log(this.name.ToString() + " died");
		Destroy (clone);
		//insert here the activity that will happen when the object dies
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (life);
		if (life < 1) {
			//Creates explosion effect on "death" if a player or an enemy
			if (this.gameObject.tag == "PlayerShip" || this.gameObject.tag == "Enemy")
				Instantiate (explosion, transform.position, transform.rotation);

			MyAI.SendMessage ("ComponentDeath");
			Die();
		}
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag ("Gun")) {
			Debug.Log (this.name.ToString () + " was hit");
			life--;
		}
	}
}
