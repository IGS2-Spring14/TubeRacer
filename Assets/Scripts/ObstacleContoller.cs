using UnityEngine;
using UnityEditor;
using System.Collections;

public class ObstacleContoller : MonoBehaviour 
{
    public enum ObstacleType { MovingPattern, SuddenMovement, RotatingPattern };
    public ObstacleType obstacleType = ObstacleType.MovingPattern;

	public string[] obstacleTypeNames = { "MovingPattern", "SuddenMovement", "RotatingPattern" };
	public int obstacleSelectionIndex = 0;

    public enum Direction { UP, DOWN, LEFT, RIGHT, FORWARD, BACKWARD };
    public Direction direction = Direction.DOWN;
    public Vector3 Velocity = Vector3.zero, prevPosition;
    public float TraverseDistance = 3000;
    private float traverseDistance = 0.0f;
    public float speed = 30f;

	public float rotationSpeed = 1.0f;
	public float rotationOffset = 5000f;
	public bool useRotationOffset = false;

	// Use this for initialization
	void Start () 
    {
        prevPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        traverseDistance += Vector3.Distance(transform.position, prevPosition);
        prevPosition = transform.position;

        if (obstacleType == ObstacleType.MovingPattern)
        {
            UpdateMovingPattern();
        }
		else if (obstacleType == ObstacleType.RotatingPattern)
		{
			UpdateRotatingPattern();
		}
	}

    private void UpdateMovingPattern()
    {
        if (direction == Direction.DOWN)
            Velocity = new Vector3(0, -1, 0);
        else if (direction == Direction.UP)
            Velocity = new Vector3(0, 1, 0);
        else if (direction == Direction.LEFT)
            Velocity = new Vector3(-1, 0, 0);
        else if (direction == Direction.RIGHT)
            Velocity = new Vector3(1, 0, 0);
        else if (direction == Direction.FORWARD)
            Velocity = new Vector3(0, 0, -1);
        else
            Velocity = new Vector3(0, 0, 1);

        if (traverseDistance < TraverseDistance)
            transform.Translate(Velocity * speed);
        else
        {
            traverseDistance = 0.0f;
            if (direction == Direction.DOWN || direction == Direction.UP)
                direction = (direction == Direction.DOWN) ? Direction.UP : Direction.DOWN;
            else if (direction == Direction.LEFT || direction == Direction.RIGHT)
                direction = (direction == Direction.LEFT) ? Direction.RIGHT : Direction.LEFT;
            else
                direction = (direction == Direction.FORWARD) ? Direction.BACKWARD : Direction.FORWARD;
        }
    }

	void UpdateRotatingPattern()
	{
		//if (useRotationOffset && transform.position != new Vector3 ((transform.position.x + rotationOffset), transform.position.y, transform.position.z))
				//		transform.position = new Vector3 ((transform.position.x + rotationOffset), transform.position.y, transform.position.z);

		transform.Rotate(Vector3.forward, rotationSpeed);

	}
}
