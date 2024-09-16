using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;

    [Header("Attack Behavior")]
    [Range(0, 2)] public float firerate;
    private float nextFireTime;

    private bool isAttacking;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking && Time.time >= nextFireTime)
        {
            Attack();
        }
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            isAttacking = true;
            Debug.Log("ATTACKING");
        }
        else if (value.canceled)
        {
            isAttacking = false;
        }
    }

    private void Attack()
    {
        // create the blaster
        Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.0105f, transform.position.z), Quaternion.identity);

        // compute for the next fire time
        nextFireTime = Time.time + firerate;
    }
}
