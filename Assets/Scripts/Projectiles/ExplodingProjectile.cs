using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : SimpleProjectile
{
    [SerializeField] private float explosionForce;
    
    private Rigidbody playerRigidbody;
    private ParticleSystem explosionParticle;
    private Vector3 explosionVector;

    void Start()
    {
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
        explosionParticle = GameObject.Find("Player").GetComponentInChildren<ParticleSystem>();
    }

    protected override void OnPlayerHit(Collision playerCollision)
    {
        explosionParticle.Play();
        PushBackPlayer(playerCollision);
        base.OnPlayerHit(playerCollision);
    }

    private void PushBackPlayer(Collision playerCollision)
    {
        explosionVector = (playerCollision.transform.position - transform.position).normalized;

        playerRigidbody.AddForce(Vector3.up * 1000, ForceMode.Impulse);
        // TODO this is not working properly, fix it later
        // playerRigidbody.AddForceAtPosition(explosionVector * explosionForce, playerRigidbody.centerOfMass, ForceMode.Impulse);
    }
}
