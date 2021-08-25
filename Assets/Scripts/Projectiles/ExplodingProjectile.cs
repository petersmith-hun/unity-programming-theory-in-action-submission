using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : SimpleProjectile
{
    protected override void OnPlayerHit(PlayerController playerController)
    {
        // TODO add "explosion" (push back the player)
        base.OnPlayerHit(playerController);
    }
}
