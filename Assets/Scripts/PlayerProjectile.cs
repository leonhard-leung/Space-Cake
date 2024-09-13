using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D projectileCollider;

    [Header("Projectile")]
    public float acceleration;

    void Update()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        projectileCollider = GetComponent<CapsuleCollider2D>();
        rigidBody.velocity = new Vector2(0, acceleration);
    }

    void LateUpdate()
    {
        if (transform.position.y > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage();
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
