using UnityEngine;
using System.Collections;

public class SpaceMine : MonoBehaviour {

	public Transform explosion;
	public GameObject target;
	//private PlayerLife targetLife;

	// Use this for initialization
	void Start () {
		//targetLife = target.GetComponent<PlayerLife> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag ("PlayerShip")) 
		{
			Debug.Log (this.name.ToString () + " was hit");
			// Deal damage to the player
			//  targetLife.NumberHit++;
			//  added to the player with tags, the right way

			//Creates explosion effect on "death" if a player or an enemy
			if (this.gameObject.tag == "PlayerShip" || this.gameObject.tag == "Enemy")
				Instantiate (explosion, transform.position, transform.rotation);

			// destroy the mine
			Destroy (this);
		}
	}
}
