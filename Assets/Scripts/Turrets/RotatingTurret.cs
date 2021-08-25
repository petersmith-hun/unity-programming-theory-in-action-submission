using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    protected override void Attack()
    {
        projectileSpawns.ForEach(Attack);
    }

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
