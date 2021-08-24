using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTurret : AbstractBaseTurret
{
    private static readonly float rotationSpeed = 0.5f;

    private GameObject turretBody;

    void Awake()
    {
        turretBody = transform.Find("Body").gameObject;
    }

    protected override void Move()
    {
        turretBody.transform.Rotate(Vector3.up * rotationSpeed);
    }
}
