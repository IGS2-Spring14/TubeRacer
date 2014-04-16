using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour {

	public bool DieByHit = false;
	public bool Log = true;
	public bool EnableCollision = true;
	public int HitMaximum = 10;
	private int NumberHit = 1;
	public AudioSource [] playerSFX;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (DieByHit) {
			if (NumberHit > HitMaximum)
			{
				Destroy (this.gameObject);
				Debug.Log("You died");
			}
		}
	}

	void OnGUI () {
		GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), (HitMaximum - NumberHit) + " lifes");
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (EnableCollision) {
			if (collision.gameObject.CompareTag ("Enemy")) {
				if (Log)
					Debug.Log ("Missile hit player " + NumberHit + " times.");
				NumberHit++;
				// put sound here
				//if (!playerSFX[0].isPlaying)
				playerSFX[0].Play();
				print (playerSFX[0].isPlaying);
			}
			else if (collision.gameObject.CompareTag ("DieWhenHit")) {
				if (Log)
					Debug.Log (collision.ToString () + " hit player " + NumberHit + " times.");
				NumberHit++;
				// put sound here
				//if (!playerSFX[0].isPlaying)
				playerSFX[0].Play();
				print (playerSFX[0].isPlaying);
			} 
			else if (collision.gameObject.CompareTag ("DieWhenHit"))
				NumberHit = HitMaximum;
			else if (Log)
				Debug.Log ("Player hit " + collision.ToString () + ".");
		}
	}
}
