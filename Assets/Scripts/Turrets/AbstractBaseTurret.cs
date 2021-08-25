using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseTurret : MonoBehaviour
{
    [SerializeField] public int damage { get; private set; }
    [SerializeField] private float attackRate;
    [SerializeField] protected Vector3 targetDirection = Vector3.forward;

    private static readonly float firstAttackDelay = 2.0f;

    private ProjectileObjectPool projectileObjectPool;
    private Transform projectileSpawn;

    private GameObject currentProjectile;

    void Start() 
    {
        projectileObjectPool = GetComponent<ProjectileObjectPool>();
        projectileSpawn = transform.Find("Body/ProjectileSpawn");
        InvokeRepeating("Attack", firstAttackDelay, attackRate);
    }

    protected virtual void Update() 
    {
        Move();
    }

    protected virtual void Attack()
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
