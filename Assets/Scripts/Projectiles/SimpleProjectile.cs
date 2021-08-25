using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public ProjectileObjectPool projectileObjectPool { private get; set; }
    
    private static readonly float projectileSpeed = 500.0f;
    
    private Vector3? targetDirection;
    [SerializeField] protected int damage;

    public void FireProjectile(Vector3 targetDirection)
    {
        this.targetDirection = targetDirection;
        StartCoroutine(DestroyExpiredProjectile());
    }

    void Update()
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

    private void OnCollisionEnter(Collision other) 
    {
        HandleProjectileHit(other);
    }

    private void HandleProjectileHit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerHit(other);
        }

        InactivateProjectile();
    }

    private IEnumerator DestroyExpiredProjectile()
    {
        yield return new WaitForSeconds(3);
        InactivateProjectile();
    }

    private void InactivateProjectile()
    {
        projectileObjectPool.InactivateProjectile(gameObject);
        targetDirection = null;
    }

    protected virtual void OnPlayerHit(Collision playerCollision)
    {
        playerCollision.gameObject
            .GetComponent<PlayerController>()
            .DamagePlayer(damage);
    }
}
