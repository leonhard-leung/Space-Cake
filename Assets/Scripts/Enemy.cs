using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D enemyRigidbody;

    [Header("Spacecraft")]
    private Vector2 targetPosition;
    private float currentSpeed;
    private float targetSpeed;
    [Range(0.01f, 1.0f)]
    public float speedChangeRate = 0.1f;
    
    [Header("Enemy State Behavior")]
    [Range(0f, 5f)]
    public float minMoveTime, maxMoveTime, minIdleTime, maxIdleTime;
    private float moveTimer, idleTimer;

    [Header("Attack Behavior")]
    public GameObject projectile;
    public float minAttackInterval = 1.0f;
    public float maxAttackInterval = 3.0f;
    private float attackTimer;

    [Header("Boundary")]
    public BoxCollider2D boundary;
    public float boundaryMargin = 0.5f;
    private Bounds boundaryBounds;


    // state of the enemy
    private enum MovementState {Idle, Moving}
    private MovementState currentState;

    void Start()
    {
        boundaryBounds = boundary.bounds;
        targetSpeed = Random.Range(0.01f, 1f);
        currentSpeed = targetSpeed;
    }

    void FixedUpdate()
    {
        // Move();

        if (currentState == MovementState.Idle)
        {

        }
        
        if (currentState == MovementState.Moving)
        {

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

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChooseNewPosition();
        }
    }

    void Attack()
    {
        // do something
    }

    void ChooseNewPosition()
    {
        float minX = boundaryBounds.min.x + boundaryMargin;
        float minY = boundaryBounds.min.y + boundaryMargin;
        float maxX = boundaryBounds.max.x - boundaryMargin;
        float maxY = boundaryBounds.max.y - boundaryMargin;

        targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        targetSpeed = Random.Range(0.5f, 1.2f);
    }

    // switch states
    void SetNewState(MovementState newState)
    {
        currentState = newState == MovementState.Moving ? MovementState.Idle : MovementState.Moving;

        if (currentState == MovementState.Moving)
        {
            moveTimer = Random.Range(minMoveTime, maxMoveTime);
        }
        else
        {
            idleTimer = Random.Range(minIdleTime, maxIdleTime);
        }
    }
}
