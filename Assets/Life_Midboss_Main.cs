using UnityEngine;
using System.Collections;

public class Life_Midboss_Main : MonoBehaviour {
	public int life = 3;
	GameObject clone;
	Midboss_AI MyAI;
	bool Vulnerable = false; 
	
	
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
		if (MyAI.Health == 1)
		{
			Vulnerable = true; 
		}


		//Debug.Log (life);
		if (life < 1) 
		{
			MyAI.SendMessage ("ComponentDeath");
			Die();
		}
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag ("Gun")) {
			if (Vulnerable)
			{
			Debug.Log (this.name.ToString () + " was hit");
			life--;
			}
		}
	}
}
