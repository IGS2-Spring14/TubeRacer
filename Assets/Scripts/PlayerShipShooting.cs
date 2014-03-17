using UnityEngine;
using System.Collections;

public class PlayerShipShooting : MonoBehaviour 
{
    public float FireCooldown = 500;
    public Rigidbody projectile;
	public float ReticleDistance = 1000;
	public bool OculusAim = false;

    private Transform target, reticle;
    private float timer;
	private Camera camera;

	// Use this for initialization
	void Start () 
    {
        reticle = transform.FindChild("Reticle");
        timer = FireCooldown;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime * 1000;

		if (!OculusAim)
			UpdateAiming ();

        if (Input.GetKeyDown(KeyCode.Mouse0) && timer < 0)
            Fire();
	}

    private void Fire()
    {
        timer = FireCooldown;

        Vector3 offset = transform.forward * 900;
		Vector3 relpos = reticle.position - transform.position;
        Rigidbody clone;
        clone = Instantiate(projectile, transform.position + offset, Quaternion.LookRotation(relpos)) as Rigidbody;
    }

	private void UpdateAiming()
	{
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);

		reticle.position = ray.GetPoint (ReticleDistance);
	}
}