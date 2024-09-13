using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [Header("Spaceship")]
    public Rigidbody2D spaceshipRigidbody;
    [Range(0,5)]
    public float thrustSpeed, lateralSpeed, decelerationFactor;
    private Vector2 direction;
    private PolygonCollider2D spaceshipCollider;

    [Header("Blasters")]
    public GameObject blastersComponent;
    [Range(0,2)]
    public float fireRate;
    private float nextFireTime;


    [Header("Playable Boundary")]
    public BoxCollider2D boundary;
    private Bounds boundaryBounds;

    void Start()
    {
        boundaryBounds = boundary.bounds;
        spaceshipCollider = GetComponent<PolygonCollider2D>();   
    }

    void Update()
    {
        MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.J) && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    /**
    Handles the movement logic of the spaceship together with boundary restriction handling
    **/
    private void MovePlayer(float horizontal, float vertical)
    {
        // create the direction vector
        direction = new Vector2(horizontal * lateralSpeed, vertical * lateralSpeed);

        // normalize the direction when going diagonally
        if (direction.magnitude > 1)
        {
            direction.Normalize();
            direction *= lateralSpeed;
        }

        // obtain the next possible position
        Vector2 nextPosition = (Vector2)transform.position + direction * Time.deltaTime;


        // check if the player is near the boundary
        bool isNearXBoundary = nextPosition.x <= boundaryBounds.min.x || nextPosition.x >= boundaryBounds.max.x;
        bool isNearYBoundary = nextPosition.y <= boundaryBounds.min.y || nextPosition.y >= boundaryBounds.max.y;

        // adjust movement based on boundary proximity
        if (isNearXBoundary)
        {
            if ((nextPosition.x <= boundaryBounds.min.x && horizontal < 0) || (nextPosition.x >= boundaryBounds.max.x && horizontal > 0))
            {
                direction.x = 0;
                Debug.Log("Boundary Y reached: restricting X movement");
            }
        }

        if (isNearYBoundary)
        {
            if ((nextPosition.y <= boundaryBounds.min.y && vertical < 0) || (nextPosition.x >= boundaryBounds.max.y && horizontal > 0))
            {
                direction.y = 0;
                Debug.Log("Boundary Y reached: restricting Y movement");
            }
        }

        // update the position of the spaceship
        spaceshipRigidbody.velocity = direction;

        // handle deceleration when no movement is registered
        if (horizontal == 0 && vertical == 0)
        {
            spaceshipRigidbody.velocity *= (1 - decelerationFactor * Time.deltaTime);
        }
    }

    /**
    Method for handling the shooting logic of the spaceship
    **/
    private void Shoot()
    {
        // create the blaster
        Instantiate(blastersComponent, new Vector3(transform.position.x, transform.position.y + 0.0105f, transform.position.z), Quaternion.identity);
        Debug.Log("Shoot");

        // compute for the next fire time
        nextFireTime = Time.time + fireRate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }

    
}
