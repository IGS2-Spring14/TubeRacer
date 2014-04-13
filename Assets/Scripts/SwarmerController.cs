using UnityEngine;
using System.Collections;

public class SwarmerController : MonoBehaviour 
{
    public float speed = 5.0f;
    public float turnRate = 0.5f;
    public float engageDistance = 15f;
    public float idleDistance = 2f;

    private enum State { idle, attack }
    private State state = State.attack;

    private float _turnRate, increaseTurnRate;
	private float _speed, increaseSpeed;
	private float fireDelay;

    private Transform player;
    private Quaternion rotation;
    private Vector3 direction;
    private float distance;

    private bool attack = true;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        _turnRate = turnRate;
	}
	
	// Update is called once per frame
	void Update () 
    {
        distance = Vector3.Distance(player.position, transform.position);

        if (state == State.attack)
        {
            _turnRate = turnRate + increaseTurnRate;
            increaseTurnRate += 0.05f;

            if (distance < idleDistance)
                state = State.idle;
        }
        else
        {
            _turnRate = 0.0f;
            increaseTurnRate = 0.0f;

            if (distance > engageDistance)
                state = State.attack;
				//Fire();
        }

         direction = player.position - transform.position;
         rotation = Quaternion.LookRotation(direction);

         transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _turnRate * Time.deltaTime);
         transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
	}

	private IEnumerable Fire()
	{
		yield return new WaitForSeconds(2f);
		//Fire
	}
}
