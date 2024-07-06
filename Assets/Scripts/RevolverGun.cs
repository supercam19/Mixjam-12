using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverGun : GunType {
    public AudioClip[] fireSounds;
    public AudioClip[] reloadSounds;

    protected override void Initialize() {
        damage = GameInfo.damage;
        fireRate = GameInfo.fireRate;
        maxAmmo = 6;
        loadedAmmo = 6;
        ammoType = REVOLVER_AMMO;
        reloadTime = GameInfo.reloadSpeed;
        reloadType = INDIVIDUAL_RELOAD;
        fireSound = fireSounds;
        reloadSound = reloadSounds;
    }
}
