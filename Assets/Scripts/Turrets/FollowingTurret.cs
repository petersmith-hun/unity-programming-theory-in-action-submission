using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// FollowingTurret is a child of AbstractBaseTurret
public class FollowingTurret : AbstractBaseTurret
{
    [SerializeField] private GameObject player;

    private static readonly float playerProximityThreshold = 200.0f;

    private GameObject turretBody;
    private Quaternion defaultTurretBodyRotation;

    private bool isPlayerNear;

    private float playerDistance;
    private Vector3 directionToPlayer;
    
    void Awake()
    {
        turretBody = transform.Find("Body").gameObject;
        defaultTurretBodyRotation = turretBody.transform.rotation;
    }
    
    protected override void FixedUpdate()
    {
        CheckIfPlayerIsNear();
        base.FixedUpdate();
    }

    // POLYMORPHISM
    // Attack method is overriden
    // FollowingTurret attacks only when the player is near
    protected override void Attack()
    {
        if (isPlayerNear)
        {
            base.Attack();
        }
    }

    // POLYMORPHISM
    // Move method is overriden and it makes the turret trying to face and follow the player.
    protected override void Move()
    {
        if (isPlayerNear)
        {
            // TODO currently only works with y=90 base rotation
            directionToPlayer = player.transform.position - turretBody.transform.position;
            directionToPlayer.y = 0;
            turretBody.transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up) * defaultTurretBodyRotation;
        }
    }

    private void CheckIfPlayerIsNear()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;
        isPlayerNear = playerDistance < playerProximityThreshold;
    }
}
