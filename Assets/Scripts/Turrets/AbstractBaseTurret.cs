using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseTurret : MonoBehaviour
{
    [SerializeField] public int damage { get; private set; }
    [SerializeField] private float attackRate;

    private static readonly float firstAttackDelay = 2.0f;

    private ProjectileObjectPool projectileObjectPool;

    private GameObject currentProjectile;

    void Start() 
    {
        projectileObjectPool = GetComponent<ProjectileObjectPool>();
        InvokeRepeating("Attack", firstAttackDelay, attackRate);
    }

    protected virtual void Update() 
    {
        Move();
    }

    protected virtual void Attack()
    {
        Attack(transform.Find("Body/ProjectileSpawn"));
    }

    protected virtual void Attack(Transform projectileSpawn)
    {
        currentProjectile = projectileObjectPool.GetProjectile();
        if (currentProjectile != null)
        {
            currentProjectile.transform.position = projectileSpawn.position;
            currentProjectile.GetComponent<SimpleProjectile>().FireProjectile(projectileSpawn.right);
        }
    }

    protected abstract void Move();
}
