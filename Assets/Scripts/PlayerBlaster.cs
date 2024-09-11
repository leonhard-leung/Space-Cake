using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlaster : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D rigidBody;

    [Header("Colliders")]
    public CapsuleCollider2D leftBlasterCollider;
    public CapsuleCollider2D rightBlasterCollider;

    [Header("Blaster Properties")]
    [Range(0,5)]
    public float laserAcceleration;

    void Start()
    {

    }
    
    void Update()
    {
        rigidBody.velocity = new Vector2(0, laserAcceleration);
    }

    void LateUpdate()
    {
        if (transform.position.y > 1) {
            Destroy(gameObject);
        }
    }
}
