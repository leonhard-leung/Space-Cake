using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D rigidBody;

    [Header("Colliders")]
    public CapsuleCollider2D leftBlasterCollider;
    public CapsuleCollider2D rightBlasterCollider;

    [Header("Projectile")]
    [Range(0,5)]
    public float acceleration;

    void Start()
    {

    }
    
    void Update()
    {
        rigidBody.velocity = new Vector2(0, acceleration);
    }

    void LateUpdate()
    {
        if (transform.position.y > 1) {
            Destroy(gameObject);
        }
    }
}
