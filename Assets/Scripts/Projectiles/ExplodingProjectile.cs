using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : SimpleProjectile
{
    [SerializeField] private float explosionForce;
    private ParticleSystem explosionParticle;

    private Vector3 explosionVector;

    void Start()
    {
        explosionParticle = GameObject.Find("Player").GetComponentInChildren<ParticleSystem>();
    }

    protected override void OnPlayerHit(Collision playerCollision)
    {
        PlayExplosionEffect();
        PushBackPlayer(playerCollision);
        base.OnPlayerHit(playerCollision);
    }

    private void PlayExplosionEffect()
    {
        explosionParticle.Play();
    }

    private void PushBackPlayer(Collision playerCollision)
    {
        explosionVector = playerCollision.transform.position - transform.position;

        playerCollision.gameObject
            .GetComponent<Rigidbody>()
            .AddForce(explosionVector * explosionForce, ForceMode.Impulse);
    }
}
