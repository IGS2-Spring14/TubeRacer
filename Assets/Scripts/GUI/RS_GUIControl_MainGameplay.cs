﻿using UnityEngine;
using System.Collections;

public class RS_GUIControl_MainGameplay : MonoBehaviour {

	public GUISkin gameGUI;
	public RS_AudioControl music;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Handles GUI functions
	void OnGUI () {
		//GUI.Box (new Rect (Screen.width / 8, Screen.height / 8, 3 * Screen.width / 4, 3 * Screen.height / 4), "MainG");

		//Displays back button
		/*
		if (GUI.Button(new Rect(0, 0, Screen.width / 4, 50), "", gameGUI.FindStyle("BackButton"))){
			//RS_GUIControl_Loading.toScreen = "Menu";
			music.RemoveObject();
			Application.LoadLevel("Menu");
		}
		*/
	}
}
