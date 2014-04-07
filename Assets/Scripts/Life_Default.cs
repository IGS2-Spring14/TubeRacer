using UnityEngine;
using System.Collections;

public class Life_Default : MonoBehaviour {
	public int life;
	GameObject clone;
	public bool VerifyAllCollisions = false;
		// Use this for initialization
	void Start () {
		clone = this.gameObject;
	}

	public void Die()
	{
		Debug.Log(this.name.ToString() + " died");
		Destroy (clone);
		//insert here the activity that will happen when the object dies
	}

	// Update is called once per frame
	void Update () {
		if (life == 0) {
			Die();
				}
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag ("Gun")) {
			//Debug.Log (this.name.ToString () + " was hit");
						life--;
				} else if (VerifyAllCollisions)
						;//Debug.Log (this.name.ToString () + " was hit by " + collision.gameObject.ToString());
	}
}
