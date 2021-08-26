using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE
// StaticTurret is a child of AbstractBaseTurret
public class StaticTurret : AbstractBaseTurret
{
    // POLYMORPHISM
    // Move method is overriden and it keeps the turret stationary.
    protected override void Move()
    {
        // this one doesn't move
    }
}
