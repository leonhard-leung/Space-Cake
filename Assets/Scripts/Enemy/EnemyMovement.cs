using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Spacecraft")]
    [SerializeField, Range(0.01f, 1f)] private float speedChangeRate;
    private Vector2 targetPosition;
    private float targetSpeed, currentSpeed;
    private bool destinationReached = false;

    [Header("Movement State")]
    [SerializeField, Range(0.01f, 5f)] private float minMoveTime;
    [SerializeField, Range(0.01f, 5f)] private float maxMoveTime;
    [SerializeField, Range(0.01f, 5f)] private float minIdleTime;
    [SerializeField, Range(0.01f, 5f)] private float maxIdleTime;
    private float timer;

    [Header("Boundary")] 
    [SerializeField] private float boundaryMargin;
    private Bounds boundary;
    private BoxCollider2D area;

    private Rigidbody2D rigidBody;

    private enum MovementState {Idle, Moving}
    private MovementState currentState = MovementState.Idle;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        area = GameObject.FindGameObjectWithTag("Enemy Boundary").GetComponent<BoxCollider2D>();
        boundary = area.bounds;

        targetSpeed = Random.Range(0.01f, 1f);
        currentSpeed = targetSpeed;
    }

    void Update()
    {
        // Process the current movement state (Idle or Moving)
        switch (currentState)
        {
            case MovementState.Idle:
                HandleIdleState();
                break;
            case MovementState.Moving:
                HandleMovingState();
                break;
        }
    }

    private void HandleIdleState()
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // Stop the enemy's movement
        rigidBody.velocity = Vector2.zero;

        // Transition to a new state if the idle timer expires
        if (timer <= 0)
        {
            ChooseNewPosition();
            SetMovementState(MovementState.Moving);
        }
    }

    private void HandleMovingState()
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // Move the enemy if the timer is active and destination is not reached
        if (timer > 0 && !destinationReached)
        {
            Move();
        }

        // Transition to a new state if move timer expires or destination is reached
        if (timer <= 0 || destinationReached)
        {
            destinationReached = false;
            SetMovementState(MovementState.Idle);
        }
    }

    private void Move()
    {
        // Obtain the direction
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Smoothly transition the speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, speedChangeRate * Time.deltaTime);

        // Set the velocity based on the smoothed speed
        rigidBody.velocity = direction * currentSpeed;

        // Set destination reached to true if the destination has been reached
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            destinationReached = true;
        }
    }

    private void ChooseNewPosition()
    {
        // Calculate the boundaries for the enemy's new position,
        // taking into account the margin to avoid the edge of the boundary
        float minX = boundary.min.x + boundaryMargin;
        float minY = boundary.min.y + boundaryMargin;
        float maxX = boundary.max.x - boundaryMargin;
        float maxY = boundary.max.y - boundaryMargin;

        // Assign a new random target position within the calculated boundaries
        targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        // Assign a new random target speed for the enemy to move towards the
        // new target position
        targetSpeed = Random.Range(0.5f, 1.2f);
    }

    private void SetMovementState(MovementState newState)
    {
        currentState = newState;

        // Reset the timer for the new state with a random duration
        if (currentState == MovementState.Moving)
        {
            timer = Random.Range(minMoveTime, maxMoveTime);
        }
        else
        {
            timer = Random.Range(minIdleTime, maxIdleTime);
        }
    }
}
