using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100f;
    private bool isInvincible = true;

    [SerializeField, Range(0.1f, 5)] private float invincibilityDuration = 0.2f;

    public void TakeDamage(float damageValue)
    {
        if (!isInvincible) 
        {
            maxHealth -= damageValue;
            Debug.Log("Damage Taken: " + damageValue + ", HP remaining: " + maxHealth);

            if (maxHealth <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Enemy Spacecraft Destroyed");
            }
        }
        else
        {
            Debug.Log("STILL INVINCIBLE MWAHAHAHHAA");
        }
    }

    private void SetInvincible(bool value)
    {
        isInvincible = value;

        if (value)
        {
            StartCoroutine(HandleInvincibility());
        }
    }

    private IEnumerator HandleInvincibility()
    {
        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make it vulnerable to player attacks once it reaches the target boundary
        if (other.CompareTag("Enemy Boundary"))
        {
            SetInvincible(true);
        }
    }
}
