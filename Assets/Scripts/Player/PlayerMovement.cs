using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;

    [Header("Spaceship")]
    [Range(0,5)] public float thrustSpeed, lateralSpeed;

    private BoxCollider2D area;
    private Bounds boundary;

    private Vector2 inputMovement;

    void Start()
    {
        // Playable Boundary
        area = GameObject.Find("Player Boundary").GetComponent<BoxCollider2D>();
        boundary = area.bounds;

        // Rigidbody
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MovePlayer(inputMovement.x, inputMovement.y);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputMovement = value.ReadValue<Vector2>();
    }

    private void MovePlayer(float xAxis, float yAxis)
    {
        // Create the direction vector
        Vector2 direction = new Vector2(xAxis * lateralSpeed, yAxis * thrustSpeed);

        // Normalize the direction when going diagonally
        if (direction.magnitude > 1)
        {
            direction.Normalize();
            direction *= lateralSpeed;
        }

        // Obtain the next possible position
        Vector2 nextPosition = (Vector2)transform.position + direction * Time.deltaTime;

        // check if the player is near the boundary
        bool isNearXBoundary = nextPosition.x <= boundary.min.x || nextPosition.x >= boundary.max.x;
        bool isNearYBoundary = nextPosition.y <= boundary.min.y || nextPosition.y >= boundary.max.y;

        // adjust movement based on boundary proximity
        if (isNearXBoundary)
        {
            if ((nextPosition.x <= boundary.min.x && xAxis < 0) || (nextPosition.x >= boundary.max.x && xAxis > 0))
            {
                direction.x = 0;
                Debug.Log("Boundary Y reached: restricting X movement");
            }
        }

        if (isNearYBoundary)
        {
            if ((nextPosition.y <= boundary.min.y && yAxis < 0) || (nextPosition.x >= boundary.max.y && yAxis > 0))
            {
                direction.y = 0;
                Debug.Log("Boundary Y reached: restricting Y movement");
            }
        }

        // update the position of the spaceship
        playerRigidbody.MovePosition(playerRigidbody.position + direction * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("PLAYER JUST BUMBED INTO THE ENEMY!!!");
        }
    }
}