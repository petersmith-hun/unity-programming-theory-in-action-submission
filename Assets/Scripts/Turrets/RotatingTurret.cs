using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// INHERITANCE
// RotatingTurret is a child of AbstractBaseTurret
public class RotatingTurret : AbstractBaseTurret
{
    private static readonly float rotationSpeed = 0.5f;

    private GameObject turretBody;
    private List<Transform> projectileSpawns;

    void Awake()
    {
        turretBody = transform.Find("Body").gameObject;
        projectileSpawns = GetProjectileSpawns();
    }

    // POLYMORPHISM
    // Attack method is overriden
    // RotatingTurret attacks in 4 directions simultaneously
    protected override void Attack()
    {
        projectileSpawns.ForEach(Attack);
    }

    // POLYMORPHISM
    // Move method is overriden and it makes the turret constantly and slowly rotating.
    protected override void Move()
    {
        turretBody.transform.Rotate(Vector3.up * rotationSpeed);
    }

    private List<Transform> GetProjectileSpawns()
    {
        return transform.Find("Body/ProjectileSpawns")
            .GetComponentsInChildren<Transform>()
            .Where(spawnTransform => spawnTransform.gameObject.name.StartsWith("ProjectileSpawn_"))
            .ToList<Transform>();
    }
}
