using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// SimpleProjectile is the parent of ExplodingProjectile
public class SimpleProjectile : MonoBehaviour
{
    // ENCAPSULATION
    public ProjectileObjectPool projectileObjectPool { private get; set; }
    
    private static readonly float projectileSpeed = 500.0f;
    
    private Vector3? targetDirection;
    [SerializeField] protected int damage;

    // ABSTRACTION
    // Firing a projectile should be done by setting the target's direction, starting the expiration coroutine,
    // and essentially moving the projectile towards the target.
    public void FireProjectile(Vector3 targetDirection)
    {
        this.targetDirection = targetDirection;
        StartCoroutine(DestroyExpiredProjectile());
    }

    void FixedUpdate()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        if (targetDirection.HasValue)
        {
            transform.position += targetDirection.Value * projectileSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        HandleProjectileHit(other);
    }

    private void HandleProjectileHit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerHit(other);
        }

        InactivateProjectile();
    }

    private IEnumerator DestroyExpiredProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        InactivateProjectile();
    }

    private void InactivateProjectile()
    {
        projectileObjectPool.InactivateProjectile(gameObject);
        targetDirection = null;
    }

    protected virtual void OnPlayerHit(Collider playerCollider)
    {
        playerCollider.gameObject
            .GetComponent<PlayerController>()
            .DamagePlayer(damage);
    }
}
