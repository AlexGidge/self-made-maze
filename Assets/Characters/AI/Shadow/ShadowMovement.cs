using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShadowMovement : CharacterMovement
{
    public float MovedirectionChangeDelay;
    public float CollisionDamage;
    private float lastDirectionChangeTime;
    

    [SerializeField]
    private Vector2 currentMovement;

    public void Start()
    {
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyMovement;
    }

    private void ApplyMovement()
    {
        ApplyMovement(currentMovement);
    }

    public void ChangeMovement()
    {
        if (lastDirectionChangeTime + MovedirectionChangeDelay < Time.time)
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);
            SetMovement(x, y);
        }
    }

    private void SetMovement(int x, int y)
    {
        lastDirectionChangeTime = Time.time;
        currentMovement = new Vector2(x, y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ChangeMovement();
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