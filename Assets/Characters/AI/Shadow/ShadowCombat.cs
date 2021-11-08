using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class ShadowCombat : CharacterCombat
{
    public float TimeBetweenAttacks;
    
    public GameObject Attack1Bullet;
    public GameObject Attack2Bullet;
    public GameObject Attack3Bullet;

    public float Attack3damage;
    public int Attack1BulletCount;
    
    public AudioClip Attack1AudioClip;
    public AudioClip Attack3AudioClip;

    public ShadowAttackSequence CurrentSequence;
    public ShadowState CurrentState;

    protected override void Initialise()
    {
        CurrentSequence = ShadowAttackSequence.Starting;
        CurrentState = ShadowState.Passive;
        TriggerManager.Current.TriggerEvents.OnRoomEntered += StartSequenceCycle;
        CharacterState = CharacterStateType.Alive;
    }
    
    public override void Die()
    {
        Game.Current.GameEvents.LevelCompleted();
        Game.Current.GameEvents.NPCDied(CharacterID);
        //TODO: Particle Effect
        //TODO: Animation
        //TODO: End game event + listener on player to add shadow
        StartCoroutine("SelfDestruct");
    }

    private IEnumerator SelfDestruct()
    {        
        yield return new WaitForSeconds(0.5f); 
        Destroy(gameObject);
            //TODO: Fadeout
    }

    public IEnumerator Attack1()
    {
        //TODO: Attack
        Debug.Log("Attack 1 Started.");
        for (int x = 0; x < Attack1BulletCount; x++)
        {
            yield return new WaitForSeconds(0.22f);
            FireBullet(Attack1Bullet, GenerateQuaternion(x));
            BulletAudioSource.PlayOneShot(Attack1AudioClip);
        }
    }

    private IEnumerator Attack2()
    {
        Debug.Log("Attack 2 Started.");
        yield return Attack1();
    }

    private IEnumerator Attack3()
    {
        Debug.Log("Attack 3 Started.");

            GameObject bullet = Instantiate(Attack3Bullet);
            bullet.transform.position = this.transform.position;
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bullet.transform.rotation = new Quaternion();
                bulletController.FireBullet(CharacterID, new Quaternion(), Attack3damage);
                BulletAudioSource.PlayOneShot(Attack3AudioClip);
                //TODO: Stop movement
            }

            yield return new WaitForSeconds(11);
    }

    private Quaternion GenerateQuaternion(int bullet)
    {
        //TODO: Learn about quaternions then rewrite.
        Quaternion direction = quaternion.RotateZ(15*bullet);
        return direction;
    }

    public void StartSequenceCycle()
    {
        if (CurrentState == ShadowState.Passive)
        {
            CurrentSequence = ShadowAttackSequence.Starting;
            StartCoroutine("StartSequence");
        }
        else
        {
            Debug.LogError("Sequence already started.");
        }
    }
    
    public IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(TimeBetweenAttacks);
        Debug.Log("Sequence Starting...");
        CurrentState = ShadowState.Attacking;
        GenerateNextSequence();
        yield return PlayCurrentSequence();
        CurrentState = ShadowState.Finished;
        StartCoroutine("StartSequence");
    }

    private IEnumerator PlayCurrentSequence()
    {
        if (CurrentState == ShadowState.Attacking)
        {
            switch (CurrentSequence)
            {
                case ShadowAttackSequence.Attack1:
                    yield return Attack1();
                    break;
                case ShadowAttackSequence.Attack2:
                    yield return Attack2();
                    break;
                case ShadowAttackSequence.Attack3:
                    yield return Attack3();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void GenerateNextSequence()
    {
        switch (CurrentSequence)
        {//TODO: a real sequence
            case ShadowAttackSequence.Attack1:
                CurrentSequence = ShadowAttackSequence.Attack2;
                break;
            case ShadowAttackSequence.Attack2:
                CurrentSequence = ShadowAttackSequence.Attack3;
                break;
            case ShadowAttackSequence.Starting:
            case ShadowAttackSequence.Attack3:
                CurrentSequence = ShadowAttackSequence.Attack1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AnimateDeath()
    {
        Debug.Log("Death animation");
    }
    
    //TODO: 3 attack sequences on repeat
}

public enum ShadowState
{
    Passive = 0,
    Attacking = 2,
    Finished = 3,
}

public enum ShadowAttackSequence
{
    Starting = 0,
    Attack1 = 1,
    Attack2 = 2,
    Attack3 = 3,
}