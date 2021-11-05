using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowCombat : CharacterCombat
{
    public float TimeBetweenAttacks;
    public GameObject BulletPrefab;
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
        yield return Attack2();
    }
    
    
    private IEnumerator Attack2()
    {
        Debug.Log("Attack 2 Started.");
        for (int x = 0; x < 10; x++)
        {
            yield return new WaitForSeconds(0.25f);
            FireBullet(BulletPrefab, GenerateQuaternion());
        }
    }

    private IEnumerator Attack3()
    {
        Debug.Log("Attack 3 Started.");
        yield return Attack2();
    }

    private Quaternion GenerateQuaternion()
    {
        return transform.rotation;
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
        Debug.Log("Sequence Starting...");
        CurrentState = ShadowState.Attacking;
        GenerateNextSequence();
        yield return PlayCurrentSequence();
        CurrentState = ShadowState.Finished;
        yield return new WaitForSeconds(TimeBetweenAttacks);
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