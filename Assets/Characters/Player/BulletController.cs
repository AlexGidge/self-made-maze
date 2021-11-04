using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    private bool fired;
    private float bulletSpeed = 5000f;
    private float multiplier = 1;

    private float Damage;

    private List<Guid> CharacterHits;

    [SerializeField] private Quaternion bulletDirection;

    void Awake()
    {
        Initialise();
        StartCoroutine(Selfdestruct());
    }

    private void Initialise()
    {
        CharacterHits = new List<Guid>();
    }

    IEnumerator Selfdestruct()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    public void FireBullet(Guid parent, Quaternion direction, float damage)
    {
        CharacterHits.Add(parent);
        Damage = damage;
        bulletDirection = direction;
        fired = true;
    }

    private void FixedUpdate()
    {
        if (fired)
        {
            Vector3 force = bulletDirection * Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(force * bulletSpeed * Time.deltaTime * multiplier);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (multiplier > 0)
        {
            multiplier -= 0.1f;
        }

        if (other.collider.gameObject.layer == (int) LayerType.Character)
        {
            CharacterCombat combat = other.collider.gameObject.GetComponent<CharacterCombat>();
            if (combat != null)
            {
                if (!CharacterHits.Contains(combat.CharacterID))
                {
                    CharacterHits.Add(combat.CharacterID);
                    combat.TakeDamage(Damage);
                }
            }
        } //TODO: Handle else error
    }
}