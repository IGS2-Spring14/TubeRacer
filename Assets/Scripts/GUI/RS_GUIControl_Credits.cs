using UnityEngine;
using System.Collections;

public class RS_GUIControl_Credits : MonoBehaviour {

	public GUISkin gameGUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Handles GUI functions
	void OnGUI () {
		GUI.Box (new Rect (Screen.width / 8, Screen.height / 8, 3 * Screen.width / 4, 3 * Screen.height / 4), "Credits");

		if (GUI.Button(new Rect(0, 0, Screen.width / 4, 50), "", gameGUI.FindStyle("BackButton")))
			Application.LoadLevel ("Menu");
	}
}
