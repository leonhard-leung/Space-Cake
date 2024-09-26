using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damageValue = 40f;
    public float acceleration;

    private Rigidbody2D rigidBody;
    private Vector2 direction;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
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
    }
}
