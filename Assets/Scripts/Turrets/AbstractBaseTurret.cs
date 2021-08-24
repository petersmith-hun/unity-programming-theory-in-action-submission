using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseTurret : MonoBehaviour
{
    [SerializeField] public int damage { get; private set; }
    [SerializeField] private float attackRate;

    private static readonly float firstAttackDelay = 2.0f;

    void Start() 
    {
        InvokeRepeating("Attack", firstAttackDelay, attackRate);
    }

    protected virtual void Update() 
    {
        Move();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerHit();
        }
    }

    protected virtual void Attack()
    {
        // TODO implement
    }

    protected abstract void Move();

    protected virtual void OnPlayerHit()
    {
        // TODO implement, override for following turret
    }
}
