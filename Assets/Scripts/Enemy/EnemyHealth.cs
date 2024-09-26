using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100f;
    private bool isInvincible = true;

    public void TakeDamage(float damageValue)
    {
        // Decrease health if not invincible
        if (!isInvincible) 
        {
            maxHealth -= damageValue;
            Debug.Log("Damage Taken: " + damageValue + ", HP remaining: " + maxHealth);

            // Remove the object if health is depleted
            if (maxHealth <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Enemy Spacecraft Destroyed");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make it vulnerable to player attacks once it reaches the target boundary
        if (other.CompareTag("Enemy Boundary"))
        {
            isInvincible = false;
            Debug.Log("Enemy is no longer invincible");
        }
    }
}
