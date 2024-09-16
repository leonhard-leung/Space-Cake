using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth;
    
    void Start()
    {
        maxHealth = 100f;
    }

    public void TakeDamage(float damageValue)
    {
        maxHealth -= damageValue;
        Debug.Log("Damage Taken: " + damageValue + ", HP remaining: " + maxHealth);

        if (maxHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Spacecraft Destroyed");
        }
    }
}
