using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    [Header("Spacecraft")]
    private Vector2 targetPosition;
    private float currentSpeed, targetSpeed;
    private bool destinationReached;
    [Range(0.01f, 1.0f)]
    public float speedChangeRate;

    [Header("Movement State")]
    private float timer;
    [Range(0.0f, 5.0f)]
    public float minMoveTime, maxMoveTime, minIdleTime, maxIdleTime;

    [Header("Boundary")]
    public BoxCollider2D area;
    public float boundaryMargin;
    private Bounds boundary;

    private enum MovementState {Idle, Moving}
    private MovementState currentState;
    
    void Start()
    {
        // Set the initial values
        rigidBody = GetComponent<Rigidbody2D>();
        destinationReached = false;
        boundary = area.bounds;
        targetSpeed = Random.Range(0.01f, 1f);
        currentSpeed = targetSpeed;
        SetMovementState(MovementState.Idle);
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
            Debug.Log("Switched to moving state");
        }
        else
        {
            timer = Random.Range(minIdleTime, maxIdleTime);
            Debug.Log("Switched to idle state");
        }
    }
}
