using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
    private GameObject leftProjectile;
    private GameObject rightProjectile;

    void Start()
    {
        // Obtain the references
        leftProjectile = transform.Find("Left Projectile")?.gameObject;
        rightProjectile = transform.Find("Right Projectile")?.gameObject;
    }

    void Update()
    {
        // Check if the left and right projectile are destroyed
        bool leftProjectileDestroyed = leftProjectile == null || !leftProjectile.activeInHierarchy;
        bool rightProjectileDestroyed = rightProjectile == null || !rightProjectile.activeInHierarchy;


        if (leftProjectileDestroyed && rightProjectileDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
