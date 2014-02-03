using UnityEngine;
using System.Collections;

public class RS_InitialLoad : MonoBehaviour {

	public static string toScreen;
	private float load;

	// Use this for initialization
	void Start () {
		toScreen = "Menu";
		load = 3f;
	}

	// Update is called once per frame
	void Update () {
		
		load -= 1f * Time.deltaTime;
		
		if (load <= 0f)
			Application.LoadLevel(toScreen);
		
	}
	
	//Handles GUI functions
	void OnGUI () {
		GUI.Label(new Rect(0,0,80,50),"Time to finish loading : " + load.ToString());
		GUI.Box (new Rect (Screen.width / 8, Screen.height / 8, 3 * Screen.width / 4, 3 * Screen.height / 4), "LOADING SCREEN!:P");
	}
}
