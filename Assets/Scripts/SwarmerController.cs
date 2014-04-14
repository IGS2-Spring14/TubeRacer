using UnityEngine;
using System.Collections;

public class SwarmerController : MonoBehaviour 
{
    public float speed = 5.0f;
	public float maxSpeed = 10.0f;
    public float turnRate = 0.5f;
    public float engageDistance = 15f;
    public float idleDistance = 2f;
	public float playerTargetOffset = 3000;
	public GameObject projectile;

    private enum State { idle, attack }
    private State state = State.attack;

    private float _turnRate, increaseTurnRate;
	private float _speed, increaseSpeed;
	private float fireDelay;
	private bool EliteSwarmer = false;

    private Transform player;
	private Vector3 playerOffset;
    private Quaternion rotation;
    private Vector3 direction;
    private float distance;

    private bool attack = true;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
		playerOffset = new Vector3 (player.position.x + Random.Range (-playerTargetOffset, playerTargetOffset),
		                            player.position.y + Random.Range (-playerTargetOffset, playerTargetOffset),
		                            player.position.z + Random.Range (-playerTargetOffset, playerTargetOffset));
        _turnRate = turnRate;
		speed = Random.Range (speed, maxSpeed);
		EliteSwarmer = (Random.Range (0, 10) < 1) ? true : false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        distance = Vector3.Distance(player.position, transform.position);

        if (state == State.attack)
        {
            _turnRate = turnRate + increaseTurnRate;
			if (EliteSwarmer)
				_turnRate = turnRate * 2f;

			if (distance < Random.Range(idleDistance, (idleDistance*1.3f)))
			{
                state = State.idle;
				StartCoroutine("IncreaseTurnRate");
				StopCoroutine("Fire");
        	}
		}
        else
        {
            _turnRate = 0.0f;
            increaseTurnRate = 0.0f;

            if (distance > Random.Range(engageDistance-(engageDistance*1.3f), (engageDistance*1.3f)))
			{
                state = State.attack;
				StartCoroutine("Fire");
				StopCoroutine("IncreaseTurnRate");
				playerOffset = new Vector3 (player.position.x + Random.Range (-playerTargetOffset, playerTargetOffset),
				                            player.position.y + Random.Range (-playerTargetOffset, playerTargetOffset),
				                            player.position.z + Random.Range (-playerTargetOffset, playerTargetOffset));
			}
        }

         direction = (player.position + playerOffset) - transform.position;
         rotation = Quaternion.LookRotation(direction);

         transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _turnRate * Time.deltaTime);
         transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
	}

	private IEnumerator Fire()
	{
		while (true)
		{
			GameObject clone;
			clone = Instantiate (projectile, transform.position, Quaternion.LookRotation((player.position + playerOffset) - transform.position)) as GameObject;
			yield return new WaitForSeconds(0.5f);
		}
	}

	private IEnumerator IncreaseTurnRate()
	{
		while (true)
		{
			increaseTurnRate += (increaseTurnRate * Random.Range(1.02f, 1.1f));
			yield return new WaitForSeconds(0.5f);
		}
	}
}
