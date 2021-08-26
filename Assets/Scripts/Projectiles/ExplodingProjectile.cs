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

    protected override void OnPlayerHit(Collider playerCollider)
    {
        explosionParticle.Play();
        PushBackPlayer(playerCollider);
        base.OnPlayerHit(playerCollider);
    }

    private void PushBackPlayer(Collider playerCollider)
    {
        explosionVector = (playerCollider.transform.position - transform.position).normalized + Vector3.up;

        // TODO this is not working properly, fix it later
        // playerRigidbody.AddForce(Vector3.up * explosionForce, ForceMode.Impulse);
        playerRigidbody.AddForce(explosionVector * explosionForce, ForceMode.Impulse);
    }
}
