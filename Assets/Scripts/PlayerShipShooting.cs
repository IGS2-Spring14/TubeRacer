using UnityEngine;
using System.Collections;

public class PlayerShipShooting : MonoBehaviour 
{
    public float FireCooldown = 500;
    public Rigidbody projectile;

    private Transform target, reticle;
    private float timer;

	// Use this for initialization
	void Start () 
    {
        reticle = transform.FindChild("Reticle");
        timer = FireCooldown;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime * 1000;

        if (Input.GetKeyDown(KeyCode.Mouse0) && timer < 0)
            Fire();
	}

    private void Fire()
    {
            timer = FireCooldown;

            Vector3 offset = transform.forward * 1000;
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position + offset, transform.rotation) as Rigidbody;
    }
}
