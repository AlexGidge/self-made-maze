using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : CharacterCombat
{
    public PlayerInput PlayerInput;
    public PlayerMovement PlayerMovement;
    public GameObject BulletPrefab;

    public GameObject PlayerShadow;

    private void OnEnable()
    {
        RegisterEvents();
        Game.Current.GameEvents.OnLevelCompleted += LevelCompleted;
    }

    private void LevelCompleted()
    {
        PlayerShadow.SetActive(true);
    }

    private void RegisterEvents()
    {
        PlayerInput.Events.OnWeaponFired += Fire;
    }

    private void OnDisable()
    {
        DeRegisterEvents();
    }
    
    private void DeRegisterEvents()
    {
        PlayerInput.Events.OnWeaponFired -= Fire;
    }

    void Fire()
    {
        Quaternion playerDirection = PlayerMovement.currentRotation;
        FireBullet(BulletPrefab, playerDirection);
    }

    public override void Die()
    {
        //TODO: PlayerDeath
        SceneManager.LoadScene("Game");
    }
}
