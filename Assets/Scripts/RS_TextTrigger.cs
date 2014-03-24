using UnityEngine;
using System.Collections;

public class RS_TextTrigger : MonoBehaviour {

	public GUISkin gameGUI;
	public float messageTime;
	public string customText;
	string text;
	float timer, letterPause;
	bool displayText, setTime;

	// Use this for initialization
	void Start () {
		text = "";
		displayText = false;
		setTime = false;

		timer = 0f;

		letterPause = 0.05f;
		messageTime *= 60f;
	}

	//Used to pull letters from the entered string to display
	IEnumerator TypeText () {
		foreach (char letter in customText.ToCharArray()) {
			text += letter;
			yield return new WaitForSeconds (letterPause);
		}      
	}

	void OnGUI () {
		//Displays text object if allowed to display it
		if (displayText)
			GUI.Label (new Rect (Screen.width / 8, Screen.height / 16, 3 * Screen.width / 4, Screen.height / 4), text, gameGUI.label);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (timer);

		//While this object is displaying text, it will check for the following
		if (displayText) {
			//Starts timer for display countdown if text is fully displayed
			if (string.Equals(customText, text) && !setTime) {
				StopCoroutine("TypeText");
				timer = messageTime;
				setTime = true;
			}

			//Counts down timer for displaying message,
			if (timer > 0f && setTime)
				timer -= 1f;
			
			//Disables text display if timer has run out
			if (timer <= 0f && setTime) {
				displayText = false;
				Destroy (this.gameObject);
			}
		}
	}

	//Called when an object with a rigidbody enters the collider space of this object
	void OnTriggerEnter (Collider collision) {
		if (collision.gameObject.tag == "Player") {
			displayText = true;
			StartCoroutine (TypeText ());
		}
	}
}