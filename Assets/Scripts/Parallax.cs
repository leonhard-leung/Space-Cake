using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxSpeed;
    private float height, depth, startingPosition, updatedYPos;
    private Vector3 offscreenPosition;

    public void Start()
    {
        // obtain the height of the sprite
        height = GetComponent<SpriteRenderer>().bounds.size.y;

        // obtain the depth of the sprite
        depth = transform.position.z;

        // subtract the y position by the height since the bottom half of the sprite is the first to be displayed in the camera
        startingPosition = transform.position.y - height;

        // calculate point of which the sprite leaves the screen entirely.
        offscreenPosition = new Vector3(transform.position.x, startingPosition + height, depth);
    }

    public void Update()
    {
        // update the y position
        updatedYPos = transform.position.y - parallaxSpeed * Time.deltaTime;

        // make changes to the sprite's position
        transform.position = new Vector3(transform.position.x, updatedYPos, depth);

        // if the sprite leaves the screen, bring it back to the starting point
        if (transform.position.y <= startingPosition)
        {
            transform.position = offscreenPosition;
        }
    }
}
