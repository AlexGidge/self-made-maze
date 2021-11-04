using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : CharacterMovement
{
    public PlayerInput PlayerInput;
    public Vector2 SpawnLocation = new Vector2(-4.5f, -12.5f);
   internal Quaternion currentRotation => transform.rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        RegisterEvents();
        transform.position = SpawnLocation;
    }

    private void RegisterEvents()
    {
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyMovement;
        EngineManager.Current.Events.EveryPhysicsUpdate += Rotate;
        PlayerInput.Events.OnLookDirection += ApplyRotation;
        PlayerInput.Events.OnLookPoint += LookAtPoint;
        PlayerInput.Events.OnDash += Dash;
    }
    
    private void ApplyMovement()
    {
        ApplyMovement(PlayerInput.MovementVector);
    }
    
    private void Dash()
    {
        Dash(PlayerInput.MovementVector);
    }
    
    private void LookAtPoint(Vector3 point)
    {
        SetLookPoint(point);
    }

    private void ApplyRotation(Vector2 direction)
    {
        LookInDirection(direction);
    }
}
