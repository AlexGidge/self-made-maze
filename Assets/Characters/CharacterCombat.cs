using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class CharacterCombat : MonoBehaviour
{
    public Guid CharacterID;
    
    public float maxHealth;
    public float MinDamage;
    public float MaxDamage;
    
    //TODO: Invulnerability time for player
    //public float InvulnerabilityTime;
    
    [SerializeField]
    private float CurrentHealth;
    
    public AudioSource BulletAudioSource;

    private void Start()
    {
        CharacterID = Guid.NewGuid();
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        
        if (CurrentHealth < 0f)
            Die();
    }

    public abstract void Die();
    
    protected void FireBullet(GameObject bulletPrefab, Quaternion direction)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bullet.transform.rotation = direction;
            bulletController.FireBullet(CharacterID, direction, CalculateDamage());
            BulletAudioSource.PlayOneShot(BulletAudioSource.clip);
        }
    }

    private float CalculateDamage()
    {
        return Random.Range(MinDamage, MaxDamage);
    }
}
