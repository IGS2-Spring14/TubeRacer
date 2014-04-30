using UnityEngine;
using System.Collections;

public class RS_StopShipOnDeath : MonoBehaviour {

	public GameObject playerShip;
	public SplineInterpolator platform;
	public PlayerLife player;
	public Camera mainCam;
	private Ray frontRay;
	private Vector3 frontPos;
	private Quaternion backRotation;

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
		//Gets position of center of screen from camera perspetive
		//if (player.GetHitNumber () <= player.HitMaximum)
		//{
			frontRay = mainCam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
			frontPos = frontRay.GetPoint (1200f);
			backRotation.SetLookRotation (playerShip.transform.forward * -1, playerShip.transform.up);
		//}

		if (player.GetHitNumber () > player.HitMaximum && !buttonsExist) {
			//Stops the player ship form moving
			platform.StopShip = true;

			//Creats restart/quit buttons
			if (platform.StopShip){
				Instantiate (button[0], frontPos + new Vector3 (0, 200, 0), backRotation);
				Instantiate (button[1], frontPos + new Vector3 (0, -200, 0), backRotation);
			}

			buttonsExist = true;
		}
	}
}
