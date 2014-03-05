using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
//	void OnGUI (){
//		if (GUI.Button (new Rect (20,40,80,20), "Shake")){
//			Shake ();
//			BackToOrigin();
//		}
//		if (GUI.Button (new Rect (20,60,80,20), "Back")){
//			BackToOrigin();
//		}
//	}

	void BackToOrigin()
	{
		//transform.localPosition = new Vector3 (0f, 0.4093f, -0.812f);
		//transform.rotation = Quaternion.Slerp (transform.rotation, new Quaternion(0,0,0,0), .5f);
		transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
	}
	
	void Update (){
				if (shake_intensity > 0) {
						//transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
						transform.rotation = new Quaternion (
				originRotation.x + Random.Range (-shake_intensity, shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity, shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity, shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity, shake_intensity) * .2f);
						shake_intensity -= shake_decay;
				} else
						BackToOrigin ();
		}
	
	void Shake(){
		//originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = .2f;
		shake_decay = 0.005f;
	}
}
