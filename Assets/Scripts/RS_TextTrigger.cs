using UnityEngine;
using System.Collections;

/*
 * Text Trigger
 * 
 * Written by: T-ZA aka Ricky Sanders
 * 
 * Description: This object is used for the displaying of custom
 * text in a text box as a GUI Label for a specified amount of
 * time. The object then destroys itself
 * */

public class RS_TextTrigger : MonoBehaviour {
	/*
	 * gameGUI		- the GUISkin that contains the Label settings for displaying text
	 * messageTime	- amount of time (in seconds) that the ful message should be displayed
	 * customText	- string of text to eb displayed
	 * textSFX		- array of sounds that will be played by this object
	 * 				  Indexes: 0 = boxEnter (sound played when box is growing)
	 * 						   1 = boxExit (sound played when box is shrinking)
	 * 						   2 = letterTyping (sound played when a letter is "typed"
	 * 						   3 = talkingSound (sound of "person" talking)
	 * text			- string actually displayed by the text box
	 * timer		- timer for displaying full message
	 * letterPause	- amount of time (in seconds) in between "typing" letters
	 * displayText	- whether to display GUI elements or not
	 * setTime		- whether the time for displaying text has been set or not
	 * growBox		- whether to increase height of text box or not
	 * shrinkBox	- whether to decrease height of text box or not
	 * boxHeight	- current hieight of text box. Used by OnGUI event to control height of the box displayed
	 * maxBoxHeight	- how high the text box should be (in pixels)
	 * */

	public GUISkin gameGUI;
	public float messageTime;
	public string customText;
	public AudioSource [] textSFX;

	string text;
	float timer, letterPause;
	bool displayText, setTime, growBox, shrinkBox;
	int boxHeight, maxBoxHeight;

	// Use this for initialization
	void Start () {
		text = "";
		displayText = false;
		setTime = false;

		timer = 0f;

		letterPause = 0.05f;
		messageTime *= 60f;

		boxHeight = 0;
		growBox = false;
		shrinkBox = false;
		maxBoxHeight = Screen.height / 4;
	}

	//Used to pull letters from the entered string to display
	IEnumerator TypeText () {
		foreach (char letter in customText.ToCharArray()) {
			text += letter;
			//if (!textSFX[2].isPlaying)
			textSFX[2].Play();
			yield return new WaitForSeconds (letterPause);
		}      
	}

	void OnGUI () {
		//Displays text object if allowed to display it with a height specified by boxHeight
		if (displayText)
			GUI.Label (new Rect (Screen.width / 8, Screen.height / 16, 3 * Screen.width / 4, boxHeight), text, gameGUI.label);
	}
	
	// Update is called once per frame
	void Update () {
		//If allowed to display text
		if (displayText) {
			//Increases box size to max height specified by the user if set to grow,
			//then begins displaying text if maximum height is reached
			if (growBox && boxHeight < maxBoxHeight) {
				boxHeight += 5;

				//Plays box growing sound while growing
				if (!textSFX[0].isPlaying)
					textSFX[0].Play();
			} else if (growBox && boxHeight >= maxBoxHeight) {
				//Corrects height if greater than maximum
				boxHeight = maxBoxHeight;

				//Stops box growing in height
				growBox = false;

				//Stops box growing sound once max height is reached and
				//sound is still playing
				if (textSFX[0].isPlaying)
					textSFX[0].Stop();

				//Begins coroutine for "typing" out message
				StartCoroutine (TypeText ());
			}

			//Decreases box height to 0 if set to shrink,
			//then destroys
			if (shrinkBox && boxHeight > 0) {
				//Resets string to be displayed so the message isn't 
				//shown while box is shrinking
				text = "";

				boxHeight -= 5;

				//Plays box shrinking sound while shrinking
				if (!textSFX[1].isPlaying)
					textSFX[1].Play();
			} else if (shrinkBox && boxHeight <= 0) {
				boxHeight = 0;
				//Stops box growing in height
				shrinkBox = false;

				//Stops box shrinking sound once max height is reached and
				//sound is still playing
				if (textSFX[1].isPlaying)
					textSFX[1].Stop();

				//Stops displaying text box
				displayText = false;

				//Destroys self
				Destroy (this.gameObject);
			}

			//While this object is at the desired height,
			//it will check for the following
			if (!growBox && !shrinkBox && boxHeight == maxBoxHeight) {
				//Starts timer for display countdown if text is fully displayed
				if (string.Equals(customText, text) && !setTime) {
					StopCoroutine("TypeText");
					timer = messageTime;
					setTime = true;
				}
				
				//Counts down timer for displaying message
				if (timer > 0f && setTime)
					timer -= 1f;
				
				//Begins shrinking text box if timer has run out
				if (timer <= 0f && setTime) {
					shrinkBox = true;
				}
			}

		}
	}

	//Called when an object with a rigidbody enters the collider space of this object
	void OnTriggerEnter (Collider collision) {
		//Begins displaying text and growing the text box
		//if the player makes contact with this object's collider
		if (collision.gameObject.tag == "Player") {
			displayText = true;
			growBox = true;
		}
	}
}