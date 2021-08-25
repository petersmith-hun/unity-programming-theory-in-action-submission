using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public ProjectileObjectPool projectileObjectPool { private get; set; }
    
    private static readonly float projectileSpeed = 80.0f;
    
    private Vector3? targetDirection;

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

    private void OnTriggerEnter(Collider other) 
    {
        HandleProjectileHit(other);
    }

    private void HandleProjectileHit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerHit(other.gameObject.GetComponent<PlayerController>());
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

    protected virtual void OnPlayerHit(PlayerController playerController)
    {
        // TODO this should be moved to the projectile controller
        // TODO maybe the turret should parameterize the projectile if some special "effect" is needed
        // TODO implement, override for following turret
    }
}
