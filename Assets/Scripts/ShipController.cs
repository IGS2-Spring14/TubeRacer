using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	public float maxAltitude = 10.0f;
	public float minAltitude = 0.5f;
    public float SPEED = 10f;
    public float boostSpeed = 25f;
    public float boostDecay = 0.05f;
    private float boostSpeedModifier = 1f;

	// NOTE: To invert any axis, use a negative number
	public Vector2 maneuverability = Vector2.one;

	float rotation = 0.0f;
	float altitude = 1.0f;

    public SplineInterpolator SI;

    void Start()
    {
        SI.SetSpeed(SPEED);
    }

	// Update is called once per frame
	void Update () 
	{
		UpdateInput();
		UpdatePosition();
	}

	void UpdateInput()
	{
		altitude += Input.GetAxis("Vertical") * maneuverability.y * Time.deltaTime;
		rotation += Input.GetAxis("Horizontal") * maneuverability.x * Time.deltaTime;

		altitude = Mathf.Clamp(altitude, minAltitude, maxAltitude);

        if (Input.GetKey(KeyCode.Space))
            boostSpeedModifier = boostSpeed;

        if (boostSpeedModifier > SPEED)
            boostSpeedModifier -= boostDecay;
        else
            boostSpeedModifier = SPEED;

        SI.SetSpeed(boostSpeedModifier);
	}

	void UpdatePosition()
	{
        var pos = transform.parent.up * -altitude;
		var rot = Quaternion.AngleAxis(rotation, transform.forward);

		pos = rot * pos;

		var rotAngle = Quaternion.LookRotation(transform.forward, -pos);

		transform.rotation = rotAngle;
		transform.position = transform.parent.position + pos;
	}
}
