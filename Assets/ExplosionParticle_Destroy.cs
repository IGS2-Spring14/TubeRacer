using UnityEngine;
using System.Collections;

/*
 * Written by: T-ZA aka Ricky Sanders
 * 
 * Intended to be placed on the Explosion Particle prefab. 
 * This script locates the child member containing the actual particle system,
 * triggers the animation, then destroys the parent object when the system has
 * finished playing.
 * */

public class ExplosionParticle_Destroy : MonoBehaviour {

	public ParticleSystem explosion;
	//bool hasBegun = false;

	void Awake () {
		//Gets particle system in child member for playing
		//explosion = GameObject.Find ("Explosion Particle").GetComponentInChildren("Asteroid Explosion");

		//Plays the particle animation
		explosion.Play ();
	}

	// Use this for initialization
	void Start () {
		//Sets playing "state" to true if the particle is playing
		//if (explosion.isPlaying)
			//hasBegun = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Destroys self if explosion "has begun playing" and the particle animation is finished
		if (!explosion.isPlaying)
			Destroy (this.gameObject);
	}
}
