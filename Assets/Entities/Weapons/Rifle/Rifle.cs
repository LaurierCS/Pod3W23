using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{

    public void Awake()
    {
        base.Start();
        fireForce = 30f;
        fireDelay = 0.3f; //delay in seconds between shots
        damage = 1f;
        shakeMagnitude = 2f;
        shakeRoughness = 2f;
        spread = 0.1f;
        magsize = 30; //shots before reloading
        reloadtime = 2f; //how long in seconds it takes to reload
        numShots = 1; //how many shots per trigger pull, > 1 for shotguns
        bulletLife = 3f; //how long the bullet should last before despawning
        spreadADSFactor = 10f; //by how much should we reduce spread when ADS?
        ammoPool = 500; //how much ammo the player can relaod from, -1 is infinite
    }
}
