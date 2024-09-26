using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float damageValue = 28f;
    public float acceleration;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(0, acceleration);

        // Remove the projectile if it goes beyond the screen
        if (transform.position.y > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Injure the enemy if the projectile hits them
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageValue);
            Destroy(gameObject);
        }
    }
}
