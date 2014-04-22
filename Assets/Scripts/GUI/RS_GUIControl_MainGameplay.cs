using UnityEngine;
using System.Collections;

public class RS_GUIControl_MainGameplay : MonoBehaviour {

	public GUISkin gameGUI;
	public RS_AudioControl music;
	GameObject playerShip;

	// Use this for initialization
	void Start () {
		//Finds player ship 
		//playerShip = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
			
	
	}

	//Handles GUI functions
	void OnGUI () {
		//Displays back button
		/*
		if (GUI.Button(new Rect(0, 0, Screen.width / 4, 50), "", gameGUI.FindStyle("BackButton"))){
			//RS_GUIControl_Loading.toScreen = "Menu";
			music.RemoveObject();
			Application.LoadLevel("Menu");
		*/
	}
}
