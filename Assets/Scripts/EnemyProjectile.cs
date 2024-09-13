using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D rigidBody;

    [Header("Colliders")]
    public Collider projectile;

    [Header("Projectile")]
    [Range(0,5)]
    public float acceleration;
    private Vector2 direction;

    void Update()
    {
        rigidBody.velocity = direction;
    }

    public void SetDirection(Vector2 targetDirection)
    {
        direction = targetDirection.normalized;
    }

    void LateUpdate()
    {
        // destroy projectile if it goes off the screen
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }

        // destroy projectile if it hits the player, the projectile of the player, or asteroid
    }
}
