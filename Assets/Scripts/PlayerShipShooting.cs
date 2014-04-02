using UnityEngine;
using System.Collections;

public class PlayerShipShooting : MonoBehaviour 
{
    public float FireCooldown = 500;
    public GameObject projectile;
	public float ReticleDistance = 1000;
	public bool OculusAim = false;

	private Transform ship;
	private Vector3 target;
	private Texture2D crossHairTex;
    private float timer;
	private Camera camera;

	private SplineInterpolator SI;

	// Use this for initialization
	void Start () 
    {
		crossHairTex = Resources.Load ("CrossHair") as Texture2D;
        timer = FireCooldown;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
        ship = GameObject.FindGameObjectWithTag("PlayerShip").transform;
		SI = transform.root.GetComponent<SplineInterpolator> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime * 1000;

		if (!OculusAim) 
			UpdateAiming();	
		else
			target = transform.position + (transform.forward * ReticleDistance);
		
        if ((Input.GetKeyDown (KeyCode.Mouse0) || Input.GetKeyDown (KeyCode.JoystickButton2)) && timer < 0) {
			//Debug.Log ("shooting");
			Fire ();
		}

		Fire ();
	}

    private void Fire()
    {
        timer = FireCooldown;

		Vector3 relpos = (target - transform.position).normalized;
		Vector3 offset = new Vector3(0, -25, 200);
        GameObject clone;
        clone = Instantiate(projectile, Camera.main.transform.position + offset, Quaternion.LookRotation(relpos)) as GameObject;
	}

	private void UpdateAiming()
	{
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);

		target = ray.GetPoint (ReticleDistance);
	}

	void OnGUI()
	{
		GUI.color = Color.red;
		if (!OculusAim)
			GUI.DrawTexture (new Rect (Input.mousePosition.x - 24, ((Input.mousePosition.y - Screen.height) * -1) - 24,
			                           	48, 48), crossHairTex, ScaleMode.ScaleToFit);
		else 
			GUI.DrawTexture (new Rect ((Screen.width / 2) + (Screen.width / 4.7f) - (crossHairTex.width / 2), 
			                           (Screen.height / 2) - (crossHairTex.width / 2), 48, 48), crossHairTex, ScaleMode.ScaleToFit);
	}
}