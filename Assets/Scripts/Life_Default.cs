using UnityEngine;
using System.Collections;

public class Life_Default : MonoBehaviour {
	public int life;
	GameObject clone;
		// Use this for initialization
	void Start () {
		clone = this.gameObject;
		life = 3;
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
		if (collision.CompareTag ("Gun")) {
						Debug.Log (this.name.ToString () + " was hit");
						life--;
				}
	}
}
