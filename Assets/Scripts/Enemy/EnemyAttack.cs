using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Game Object Reference")]
    public GameObject player;

    [Header("Attack State")]
    private float timer;
    [Range(0.0f, 5.0f)]
    public float minCooldownInterval, maxCooldownInterval, minAtkInterval, maxAtkInterval;
    
    [Header("Attack Behavior")]
    public GameObject projectilePrefab;
    [Range(0.0f, 2.0f)]
    public float fireRate;
    private float nextFireTime;

    private enum AttackState {Cooldown, Attack}
    private AttackState currentState;

    void Start()
    {
        // Set the initial values
        SetAttackState(AttackState.Cooldown);
    }

    void Update()
    {
        // Process the current attack state (Cooldown or Attack)
        switch (currentState)
        {
            case AttackState.Cooldown:
                HandleCooldownState();
                break;
            case AttackState.Attack:
                HandleAttackState();
                break;
        }
    }

    private void HandleCooldownState()
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // Transition to a new state if the timer expires
        if (timer <= 0)
        {
            SetAttackState(AttackState.Attack);
        }
    }

    private void HandleAttackState()
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // Attack if the timer is active
        if (timer > 0)
        {
            // Only attack if time is greater than the next fire time
            if (Time.time >= nextFireTime)
            {
                Attack();
            }
        }

        // Transition to a new state if timer expires
        if (timer <= 0)
        {
            SetAttackState(AttackState.Cooldown);
        }
    }

    void Attack()
    {
        // obtain the direction of the attack
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // create the projectile prefab
        GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.0105f), Quaternion.identity);

        // get the EnemyBlaster script component from the instantiated projectile and set the direction
        EnemyProjectile script = projectile.GetComponent<EnemyProjectile>();
        script.SetDirection(direction);
        
        Debug.Log("Shoot");

        // compute for the next fire time
        nextFireTime = Time.time + fireRate;
    }

    private void SetAttackState(AttackState newState)
    {
        currentState = newState;

        if (currentState == AttackState.Attack)
        {
            timer = Random.Range(minAtkInterval, maxAtkInterval);
            Debug.Log("Switched to attack state");
        }
        else
        {
            timer = Random.Range(minCooldownInterval, maxCooldownInterval);
            Debug.Log("Switched to cooldown state");
        }
    }
}
