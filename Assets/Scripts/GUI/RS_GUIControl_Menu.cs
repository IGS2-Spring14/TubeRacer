using UnityEngine;
using System.Collections;

public class RS_GUIControl_Menu : MonoBehaviour {

	//public GameObject loading;
	public Texture menuBG;
	public RS_AudioControl music;
	public GUISkin gameGUI;

	// Use this for initialization
	void Start () {
		//loading = GameObject.Find("loadScreen");
		//RS_GUIControl_Loading.toScreen = "Menu";
		//music = GameObject.Find("MenuMusicController");

	}
	
	// Update is called once per frame
	void Update () {

	}

	//Handles GUI functions
	void OnGUI () {
		GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), menuBG, ScaleMode.StretchToFill);

		//code for main menu
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2, Screen.width / 4, 50), "", gameGUI.FindStyle("StartButton"))){
			//RS_GUIControl_Loading.toScreen = "Basic_2";
			music.RemoveObject();
			Application.LoadLevel("Basic_2_REDUX");
		}
		
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2 + 70, Screen.width / 4, 50), "", gameGUI.FindStyle("OptionsButton"))){
			//RS_GUIControl_Loading.toScreen = "Options";
			Application.LoadLevel("Options");
		}
		
		if (GUI.Button (new Rect(3 * Screen.width / 8, Screen.height / 2 + 140, Screen.width / 4, 50), "", gameGUI.FindStyle("CreditsButton"))){
			//RS_GUIControl_Loading.toScreen = "Credits";
			Application.LoadLevel("Credits");
		}

	}

}
