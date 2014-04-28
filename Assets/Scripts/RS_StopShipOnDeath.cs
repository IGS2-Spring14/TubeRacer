using UnityEngine;
using System.Collections;

public class RS_StopShipOnDeath : MonoBehaviour {

	public GameObject playerShip;
	public SplineInterpolator platform;
	public PlayerLife player;

	//Index 0 - Restart Button
	//Index 1 - Quit Button
	public Transform [] button;
	bool buttonsExist;
	
	void Awake () {
		buttonsExist = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetHitNumber () == player.HitMaximum && !buttonsExist) {
			//Stops the player ship form moving
			platform.StopShip = true;

			//Creats restart/quit buttons
			if (platform.StopShip){
				Instantiate (button[0], playerShip.transform.position + new Vector3 (0, -750, 1000), button[0].rotation);
				Instantiate (button[1], playerShip.transform.position + new Vector3 (0, -1000, 1000), button[1].rotation);
			}

			buttonsExist = true;
		}
	}
}
