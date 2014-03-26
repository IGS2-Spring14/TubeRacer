using UnityEngine;
using System.Collections;

public class DisableOculusIfUndetected : MonoBehaviour {
	GameObject enableRift;
	GameObject disableRift;
	void Start () {
		enableRift = GameObject.Find ("OculusEnable");
		disableRift = GameObject.Find ("OculusDisable");
		if (OVRDevice.SensorCount > 0) {
			Debug.Log("Oculus Rift named \"" + OVRDevice.DisplayDeviceName + "\" was detected. Initializing.");
						disableRift.SetActive (false);
						enableRift.SetActive (true);
				} else {
						disableRift.SetActive (true);
						enableRift.SetActive (false);
						Debug.Log ("Oculus Rift isn't present or wasn't detected. Switching to regular mode.");
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
