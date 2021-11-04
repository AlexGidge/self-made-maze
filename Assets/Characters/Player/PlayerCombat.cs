using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    public PlayerInput PlayerInput;
    
    public PlayerMovement PlayerMovement;

    public AudioSource CombatSFXSource;

    public GameObject BulletPrefab;
    
    private void Start()
    {
        RegisterEvents();
        base.BulletAudioSource = CombatSFXSource;
    }

    private void RegisterEvents()
    {
        PlayerInput.Events.OnWeaponFired += Fire;
    }

    void Fire()
    {
        Quaternion playerDirection = PlayerMovement.currentRotation;
        FireBullet(BulletPrefab, playerDirection);
    }


}
