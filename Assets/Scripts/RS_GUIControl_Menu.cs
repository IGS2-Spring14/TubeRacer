using UnityEngine;
using System.Collections;

public class RS_GUIControl_Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	//Handles GUI functions
	void OnGUI () {

		//code for main menu
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2, Screen.width / 4, 50), "Play"))
			Application.LoadLevel("MainGameplay");
		
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2 + 70, Screen.width / 4, 50), "Options"))
				Application.LoadLevel("Options");
		
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2 + 140, Screen.width / 4, 50), "Credits"))
			Application.LoadLevel("Credits");

	}

}
