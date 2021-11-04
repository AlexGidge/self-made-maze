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

    public float BulletDelay;
    private float lastBulletTime;

    public float InvulnerabilityTime;
    private float lastDamageTakenTime;
    
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
        if (lastDamageTakenTime + InvulnerabilityTime < Time.time)
        {
            CurrentHealth -= damage;
            //TODO: TakeDamage FX / Audio
            //TODO: Text damage popups

            if (CurrentHealth < 0f)
                Die();
        }
    }

    public abstract void Die();

    protected void FireBullet(GameObject bulletPrefab, Quaternion direction)
    {
        if (lastBulletTime + BulletDelay < Time.time)
        {
            lastBulletTime = Time.time;
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
    }

    private float CalculateDamage()
    {
        return Random.Range(MinDamage, MaxDamage);
    }
}
