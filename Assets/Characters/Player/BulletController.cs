using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float lifetime;
    public float bulletSpeed;
    public float ShootRange;
    private Vector2 startLocation;
    
    
    //TODO: Bullet max distance?
    
    private bool fired;
    private float multiplier = 1;

    private float Damage;

    public Guid Parent;
    private List<Guid> CharacterHits;

    [SerializeField] private Quaternion bulletDirection;

    void Awake()
    {
        Initialise();
        StartCoroutine("Selfdestruct");
    }

    private void Initialise()
    {
        CharacterHits = new List<Guid>();
        Game.Current.GameEvents.OnNPCDied += NPCDied;
    }
    
    private void OnDisable()
    {
        Game.Current.GameEvents.OnNPCDied -= NPCDied;
    }

    private void NPCDied(Guid characterId)
    {
        if (characterId == Parent)
        {
            Destroy();
        }
    }
    IEnumerator Selfdestruct()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy();
        //TODO: Fadeout
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }


    public void FireBullet(Guid parent, Quaternion direction, float damage)
    {
        startLocation = transform.position;
        Parent = parent;
        CharacterHits.Add(parent);
        Damage = damage;
        bulletDirection = direction;
        fired = true;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, startLocation) > ShootRange)
        {
            Destroy();
        }
        
        if (fired)
        {
            Vector3 force = bulletDirection * Vector3.up;
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if(rigidbody != null)
                rigidbody.AddForce(force * bulletSpeed * Time.deltaTime * multiplier);
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) LayerType.Character)
        {
            CharacterCombat combat = other.gameObject.GetComponent<CharacterCombat>();
            if (combat != null)
            {
                if (!CharacterHits.Contains(combat.CharacterID))
                {
                    CharacterHits.Add(combat.CharacterID);
                    combat.TakeDamage(Damage);
                }
            }
        }
    }
}