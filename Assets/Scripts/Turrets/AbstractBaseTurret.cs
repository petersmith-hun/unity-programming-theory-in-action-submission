using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
// AbstractBaseTurret is the parent of FollowingTurret, RotatingTurret and StaticTurret.
// Turrets have 2 abstract behaviors, Move and Attack.
public abstract class AbstractBaseTurret : MonoBehaviour
{
    [SerializeField] private float attackRate;

    private static readonly float firstAttackDelay = 2.0f;

    private ProjectileObjectPool projectileObjectPool;

    private GameObject currentProjectile;

    void Start() 
    {
        projectileObjectPool = GetComponent<ProjectileObjectPool>();
        InvokeRepeating("Attack", firstAttackDelay, attackRate);
    }

    protected virtual void FixedUpdate() 
    {
        if (GameManager.instance.isGameActive)
        {
            Move();
        }
    }

    // POLYMORPHISM + ABSTRACTION
    // Attack method is overloaded
    // This one spawns the projectile in the looking direction of the attached projectile spawn game object.
    // This method also represents one of the basic behaviors of turrets, that's why it's abstracted.
    protected virtual void Attack()
    {
        Attack(transform.Find("Body/ProjectileSpawn"));
    }

    // POLYMORPHISM + ABSTRACTION
    // Attack method is overloaded
    // This one spawns the projectile in the looking direction of a specified Transform object.
    protected virtual void Attack(Transform projectileSpawn)
    {
        if (!GameManager.instance.isGameActive)
        {
            return;
        }
        
        currentProjectile = projectileObjectPool.GetProjectile();
        if (currentProjectile != null)
        {
            currentProjectile.transform.position = projectileSpawn.position;
            currentProjectile.GetComponent<SimpleProjectile>().FireProjectile(projectileSpawn.right);
        }
    }

    // POLYMORPHISM + ABSTRACTION
    // This method is abstract, every concrete implementation must override it.
    // This method also represents one of the basic behaviors of turrets, that's why it's abstracted.
    protected abstract void Move();
}
