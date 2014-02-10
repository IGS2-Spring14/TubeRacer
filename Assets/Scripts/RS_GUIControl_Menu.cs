using UnityEngine;
using System.Collections;

public class RS_GUIControl_Menu : MonoBehaviour {

	//public GameObject loading;
	public Texture menuBG;

	// Use this for initialization
	void Start () {
		//loading = GameObject.Find("loadScreen");
		//RS_GUIControl_Loading.toScreen = "Menu";

	}
	
	// Update is called once per frame
	void Update () {

	}

	//Handles GUI functions
	void OnGUI () {
		GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), menuBG, ScaleMode.StretchToFill);

		//code for main menu
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2, Screen.width / 4, 50), "Play")){
			RS_GUIControl_Loading.toScreen = "Basic_2";
			Application.LoadLevel("Loading");
		}
		
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2 + 70, Screen.width / 4, 50), "Options")){
			RS_GUIControl_Loading.toScreen = "Options";
			Application.LoadLevel("Loading");
		}
		
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2 + 140, Screen.width / 4, 50), "Credits")){
			RS_GUIControl_Loading.toScreen = "Credits";
			Application.LoadLevel("Loading");
		}

	}

}
