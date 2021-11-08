using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : CharacterCombat
{
    public PlayerInput PlayerInput;
    public PlayerMovement PlayerMovement;
    public GameObject BulletPrefab;
    public GameObject PlayerShadow;
    
    protected override void Initialise()
    {
        RegisterEvents();
        Game.Current.GameEvents.OnLevelCompleted += LevelCompleted;
        CharacterState = CharacterStateType.Alive;
    }

    private void Update()
    {
        Fire();
    }

    private void LevelCompleted()
    {
        PlayerShadow.SetActive(true);
    }

    private void RegisterEvents()
    {
        PlayerInput.Events.OnWeaponFired += StartFiring;
        PlayerInput.Events.OnWeaponFireStopped += StopFiring;
    }

    private void OnDisable()
    {
        DeRegisterEvents();
    }
    
    private void DeRegisterEvents()
    {
        PlayerInput.Events.OnWeaponFired -= StartFiring;
        PlayerInput.Events.OnWeaponFireStopped -= StopFiring;
        
    }

    private bool firing;

    private void StartFiring()
    {
        firing = true;
    }

    void Fire()
    {
        if (firing)
        {
            Quaternion playerDirection = PlayerMovement.currentRotation;
            FireBullet(BulletPrefab, playerDirection);
        }
    }

    void StopFiring()
    {
        firing = false;
    }
    
    public override void Die()
    {
        //TODO: PlayerDeath
        SceneManager.LoadScene("Game");
    }
}
