using UnityEngine;
using System.Collections;

public class RS_PositionButton : MonoBehaviour {

	public Transform player;
	public int xOff, yOff, zOff;
	bool posSet;

	// Use this for initialization
	void Start () {
		//Initial check for position
		posSet = CheckPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (posSet);

		//If the correct position isn't set, then...well...do the thing
		if (!posSet) {
			//Moves the transform a distance equal to difference between current coordinate and
			//target's position plus desired offset (this accountrs for if the transform is either behind
			//or beyond the intended location)
			/*
			transform.Translate (new Vector3 (this.transform.position.x - (player.transform.position.x + xOff),
			                                  this.transform.position.y - (player.transform.position.y + yOff),
			                                  this.transform.position.z - (player.transform.position.z + zOff)));
			**/


			//Checks X position
			if (this.transform.position.x < player.transform.position.x + xOff)
				transform.Translate(Vector3.right * (this.transform.position.x - (player.transform.position.x + xOff)) * Time.deltaTime);
			else if (this.transform.position.x > player.transform.position.x + xOff)
				transform.Translate(Vector3.left * (this.transform.position.x - (player.transform.position.x + xOff)) * Time.deltaTime);
			/*
			//Checks Y position
			if (this.transform.position.y < player.transform.position.y + yOff)
				transform.Translate(Vector3.up * (this.transform.position.y - (player.transform.position.y + yOff)) * Time.deltaTime);
			else if (this.transform.position.y > player.transform.position.y + yOff)
				transform.Translate(Vector3.down * (this.transform.position.y - (player.transform.position.y + yOff)) * Time.deltaTime);
			**/

			//Checks Z position
			if (this.transform.position.z < player.transform.position.z + zOff)
				transform.Translate(Vector3.forward * (this.transform.position.z - (player.transform.position.z + zOff)) * Time.deltaTime);
			else if (this.transform.position.z > player.transform.position.z + zOff)
				transform.Translate(Vector3.back * (this.transform.position.z - (player.transform.position.z + zOff)) * Time.deltaTime);

			//Checks to see if the button is in right position
			posSet = CheckPosition ();
		}
	}

	bool CheckPosition () {
		//If the transform is not in desired position, return false
		//Otherwise, return true
		if (this.transform.position.x != (player.transform.position.x + xOff))
			return false;
		/*
		if (this.transform.position.y != (player.transform.position.y + yOff))
			return false;**/

		if (this.transform.position.z != (player.transform.position.z + zOff))
			return false;

		return true;
	}
	
}
