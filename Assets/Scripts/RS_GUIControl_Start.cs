using UnityEngine;
using System.Collections;

public class RS_GUIControl_Start : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Handles GUI functions
	void OnGUI () {
		GUI.Box (new Rect (Screen.width / 8, Screen.height / 8, 3 * Screen.width / 4, 3 * Screen.height / 4), "LOADING SCREEN!:P");
	}
}
