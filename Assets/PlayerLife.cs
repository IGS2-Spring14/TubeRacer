using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour {

	public bool DieByHit = false;
	public bool Log = true;
	public bool EnableCollision = true;
	public int HitMaximum = 10;
	private int NumberHit = 0;
	private GameObject[] targets, enemies;
	public AudioSource [] playerSFX;
	// Use this for initialization
	void Start () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		targets = GameObject.FindGameObjectsWithTag ("Target");
	}
	
	// Update is called once per frame
	void Update () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		targets = GameObject.FindGameObjectsWithTag ("Target");
		//Debug.Log (playerSFX[0].isPlaying);
		if (DieByHit) {
			if (NumberHit >= HitMaximum)
			{
				Destroy (this.gameObject);
				Debug.Log("You died");
			}
		}
	}

	void OnGUI () {
		if (HitMaximum - NumberHit == 1)
			GUI.Box (new Rect (Screen.width - 350,Screen.height - 150,100,55), (HitMaximum - NumberHit) + " life\n" + enemies.Length.ToString() + " enemies\n" + targets.Length.ToString()+ " targets");
		else
			GUI.Box (new Rect (Screen.width - 350,Screen.height - 150,100,55), (HitMaximum - NumberHit) + " lives\n" + enemies.Length.ToString() + " enemies\n" + targets.Length.ToString()+ " targets");
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (EnableCollision) {
			if (collision.gameObject.CompareTag ("Enemy")||collision.gameObject.CompareTag ("Spacemine")) {
				if (Log)
					Debug.Log ("Missile hit player " + NumberHit + " times.");
				NumberHit++;
				// put sound here
				//if (!playerSFX[0].isPlaying)
				playerSFX[0].Play();
				Debug.Log (playerSFX[0].isPlaying);
			}
			else if (collision.gameObject.CompareTag ("DieWhenHit")) {
				if (Log)
					Debug.Log (collision.ToString () + " hit player " + NumberHit + " times.");
				NumberHit++;
				playerSFX[0].Play();
				Debug.Log (playerSFX[0].isPlaying);
				// put sound here
				//if (!playerSFX[0].isPlaying)
				//playerSFX[0].Play();
				//print (playerSFX[0].isPlaying);
			} 
			else if (Log)
				Debug.Log ("Player hit " + collision.ToString () + ".");
		}
	}
}
