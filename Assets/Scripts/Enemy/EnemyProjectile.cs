using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damageValue = 40f;

    private Rigidbody2D rigidBody;
    private CircleCollider2D projectileCollider;
    private Vector2 direction;
    public float acceleration;

    void Start()
    {
        // Set the initial values
        rigidBody = GetComponent<Rigidbody2D>();
        projectileCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // Update the direction of the projectile
        rigidBody.velocity = direction;

        // Destroy projectile if it goes off the screen
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 targetDirection)
    {
        direction = targetDirection.normalized * acceleration;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Injure the player if the projectile hits them
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageValue);
            Destroy(gameObject);
        }

        // // Destroy the projectile if it comes contact with the player's projectile
        // if (other.CompareTag("Player Projectile"))
        // {
        //     Destroy(gameObject);
        // }
    }
}
