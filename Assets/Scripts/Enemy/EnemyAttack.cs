using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack State")]
    [SerializeField, Range(0, 5f)] private float minCooldownInterval;
    [SerializeField, Range(0, 5f)] private float maxCooldownInterval;
    [SerializeField, Range(0, 5f)] private float minAtkInterval;
    [SerializeField, Range(0, 5f)] private float maxAtkInterval;
    private float timer;

    [Header("Attack Behavior")]
    [SerializeField, Range(0, 2f)] private float fireRate;
    private float nextFireTime;

    private GameObject player;
    private GameObject projectilePrefab;

    private enum AttackState {Cooldown, Attack}
    private AttackState currentState = AttackState.Cooldown;

    private bool inBoundary = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Enemy Projectile");
    }

    void Update()
    {
        // Process the current attack state (Cooldown or Attack)
        if (inBoundary)
        {
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
    }

    private void HandleCooldownState()
    {
        timer -= Time.deltaTime;

        // Change cooldown state to attack state
        if (timer <= 0)
        {
            SetAttackState(AttackState.Attack);
        }
    }

    private void HandleAttackState()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            // Only attack if time is greater than the next fire time
            if (Time.time >= nextFireTime)
            {
                Attack();
            }
        }

        // Change attack state to cooldown state
        if (timer <= 0)
        {
            SetAttackState(AttackState.Cooldown);
        }
    }

    void Attack()
    {
        // Obtain the direction of the attack
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Create the projectile prefab
        GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.0105f), Quaternion.identity);

        // Get the EnemyBlaster script component from the instantiated projectile and set the direction
        EnemyProjectile script = projectile.GetComponent<EnemyProjectile>();
        script.SetDirection(direction);

        // Compute for the next fire time
        nextFireTime = Time.time + fireRate;
    }

    private void SetAttackState(AttackState newState)
    {
        currentState = newState;

        // Reset the timer with a random value depending on the current state
        if (currentState == AttackState.Attack)
        {
            timer = Random.Range(minAtkInterval, maxAtkInterval);
        }
        else
        {
            timer = Random.Range(minCooldownInterval, maxCooldownInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Declare that the enemy is inside the boundary
        if (other.CompareTag("Enemy Boundary"))
        {
            inBoundary = true;
        }
    }
}
