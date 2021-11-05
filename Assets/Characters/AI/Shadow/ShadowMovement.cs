using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Random = UnityEngine.Random;

public class ShadowMovement : CharacterMovement
{
    public float MovedirectionChangeDelay;
    public float CollisionDamage;
    public float WakeDelay;
    private float lastDirectionChangeTime;

    [SerializeField]
    private Vector2 currentMovement;

    public void Start()
    {
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyMovement;
        TriggerManager.Current.TriggerEvents.OnRoomEntered += RoomEntered;
    }

    private void RoomEntered()
    {
        StartCoroutine("Awaken");
    }

    private IEnumerator Awaken()
    {
        yield return new WaitForSeconds(WakeDelay);
        SetMovement(1,1);
    }

    private void ApplyMovement()
    {
        if (Game.Current.GameState == GameStateType.Playing)
        {
            ApplyMovement(currentMovement);
        }
    }

    public void ChangeMovement()
    {
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        SetMovement(x, y);

    }

    private void SetMovement(int x, int y)
    {
        lastDirectionChangeTime = Time.time;
        currentMovement = new Vector2(x, y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) LayerType.Wall || other.gameObject.tag == "Player")
        {
            if (lastDirectionChangeTime + MovedirectionChangeDelay < Time.time)
            {
                ChangeMovement();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == (int) LayerType.Wall)
        {
            SetMovement(0, 0);
        }
        else if(other.collider.gameObject.tag == "Player")//TODO: Magic string
        {
            PlayerCombat playerCombat = other.collider.gameObject.GetComponent<PlayerCombat>();
            playerCombat.TakeDamage(CollisionDamage);
        }
    }
    
    //TODO: Sleep, Audio effect & wake up if player in bigger trigger
}


#if UNITY_EDITOR
[CustomEditor(typeof(ShadowMovement))]
class ShadowMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var shadowMovement = (ShadowMovement) target;
        if (shadowMovement == null)
            return;
        
        if (GUILayout.Button("Change Direction"))
        {
            shadowMovement.ChangeMovement();
        }
        
        DrawDefaultInspector();
    }
}
#endif