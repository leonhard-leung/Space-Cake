using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Behavior")]
    [SerializeField, Range(0, 2f)] private float firerate;
    private float nextFireTime;

    private GameObject projectilePrefab;

    private bool isAttacking;

    void Start()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Player Projectile");
    }

    void Update()
    {
        if (isAttacking && Time.time >= nextFireTime)
        {
            Attack();
        }
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        // Set isAttacking to true if the key is pressed else, set it to false
        if (value.started)
        {
            isAttacking = true;
        }
        else if (value.canceled)
        {
            isAttacking = false;
        }
    }

    private void Attack()
    {
        // Create the prefab
        Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.0105f, transform.position.z), Quaternion.identity);

        // Compute for the next fire time
        nextFireTime = Time.time + firerate;
    }
}
