using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth;

    void Start()
    {
        maxHealth = 500f;
    }

    public void TakeDamage(float damageValue)
    {
        maxHealth -= damageValue;
        Debug.Log("Damage Taken: " + damageValue + ", HP remaining: " + maxHealth);

        if (maxHealth <= 0)
        {
            Debug.Log("Player Defeated");
        }
    }
}
