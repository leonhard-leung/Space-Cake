using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth;
    private float damageValue;
    
    void Start()
    {
        // Set the initial values
        maxHealth = 100f;
        damageValue = 10f;
    }

    public void TakeDamage()
    {
        // Decrease the max health
        maxHealth -= damageValue;
        Debug.Log("Damage Taken: " + damageValue + ", HP remaining: " + maxHealth);

        // Destroy the game object if health reaches or goes below zero
        if (maxHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Spacecraft Destroyed");
        }
    }
}
