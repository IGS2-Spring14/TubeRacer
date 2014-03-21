using UnityEngine;
using System.Collections;

public class PlayerShipShooting : MonoBehaviour 
{
    public float FireCooldown = 500;
    public GameObject projectile;
	public float ReticleDistance = 1000;
	public bool OculusAim = false;

    private Transform ship, reticle;
    private float timer;
	private Camera camera;

	// Use this for initialization
	void Start () 
    {
        reticle = transform.FindChild("Reticle");
        timer = FireCooldown;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
        ship = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime * 1000;

		if (!OculusAim) 
			UpdateAiming();		
		
        if ((Input.GetKeyDown (KeyCode.Mouse0) || Input.GetKeyDown (KeyCode.JoystickButton2)) && timer < 0) {
			Debug.Log ("shooting");
			Fire ();
		}
	}

    private void Fire()
    {
        timer = FireCooldown;

		Vector3 relpos = (reticle.position - ship.position).normalized;
        GameObject clone;
        clone = Instantiate(projectile, ship.position, Quaternion.LookRotation(relpos)) as GameObject;
    }

	private void UpdateAiming()
	{
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);

		reticle.position = ray.GetPoint (ReticleDistance);
	}
}