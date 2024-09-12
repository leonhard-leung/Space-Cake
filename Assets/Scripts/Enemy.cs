using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Rigidbody Reference")]
    public Rigidbody2D enemyRigidbody;

    [Header("Spacecraft Properties")]
    private Vector2 targetPosition;
    private float currentSpeed, targetSpeed;
    private bool destinationReached = false;
    [Range(0.01f, 1.0f)]
    public float speedChangeRate;
    
    [Header("State Behavior")]
    private float moveTimer, idleTimer;
    [Range(0f, 5f)]
    public float minMoveTime, maxMoveTime, minIdleTime, maxIdleTime;

    [Header("Attack Behavior")]
    public GameObject projectile;
    private float attackTimer;
    [Range(0f, 5f)]
    public float minAtkInterval, maxAtkInterval;
    

    [Header("Boundary")]
    public BoxCollider2D boundary;
    public float boundaryMargin;
    private Bounds boundaryBounds;

    private enum MovementState {Idle, Moving}
    private MovementState currentState = MovementState.Idle;

    void Start()
    {
        // Initialize boundary bounds of the enemy's movement area
        boundaryBounds = boundary.bounds;

        // Set initial target and current speeds
        targetSpeed = Random.Range(0.01f, 1f);
        currentSpeed = targetSpeed;

        // Start in the Idle state
        SetNewState(currentState);
    }

    void Update()
    {
        // Process the current state (Idle or Moving)
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
        // Decrease the idle timer
        idleTimer -= Time.deltaTime;

        // Stop the enemy's movement
        enemyRigidbody.velocity = Vector2.zero;

        // Transition to a new state if the idle timer expires
        if (idleTimer <= 0)
        {
            ChooseNewPosition();
            SetNewState(currentState);
        }
    }

    private void HandleMovingState()
    {
        // Decrease the move timer
        moveTimer -= Time.deltaTime;

        // Move the enemy if the move timer is active and destination is not reached
        if (moveTimer > 0 && !destinationReached) 
        {
            Move();
        }

        // Transition to a new state if move timer expires or destination is reached
        if (moveTimer <= 0 || destinationReached)
        {
            destinationReached = false;
            SetNewState(currentState);
        }
    }

    void Move()
    {
        // obtain the direction
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // smoothly transition the speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, speedChangeRate * Time.deltaTime);

        // set the velocity based on the smoothed speed
        enemyRigidbody.velocity = direction * currentSpeed;

        // set destination reached to true if the destination has been reached
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            destinationReached = true;
        }
    }

    void Attack()
    {
        // do something
    }

    void ChooseNewPosition()
    {
        // Calculate the boundaries for the enemy's new position,
        // taking into account the margin to avoid the edge of the boundary.
        float minX = boundaryBounds.min.x + boundaryMargin;
        float minY = boundaryBounds.min.y + boundaryMargin;
        float maxX = boundaryBounds.max.x - boundaryMargin;
        float maxY = boundaryBounds.max.y - boundaryMargin;

        // Assign a new random target position within the calculated boundaries
        targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        // Assign a new random target speed for the enemy to move towards the new target position
        targetSpeed = Random.Range(0.5f, 1.2f);
    }

    void SetNewState(MovementState newState)
    {
        // Determine the new state (Idle or Moving) based on the provided newState
        currentState = newState == MovementState.Moving ? MovementState.Idle : MovementState.Moving;

        // Reset the timer for the new state with a random duration.
        if (currentState == MovementState.Moving)
        {
            moveTimer = Random.Range(minMoveTime, maxMoveTime);
            Debug.Log("Switched to moving state");
        }
        else
        {
            idleTimer = Random.Range(minIdleTime, maxIdleTime);
            Debug.Log("Switched to idle state");
        }
    }
}
