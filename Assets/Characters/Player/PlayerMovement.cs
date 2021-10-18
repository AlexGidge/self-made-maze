using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public CharacterMovement CharacterMovement;

    public Rigidbody2D PlayerRigidBody;
    public float PlayerMoveSpeed;
    public float PlayerRotationSpeed;

    [SerializeField] private Vector2 currentMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        RegisterEvents();
        CharacterMovement.Initialise(PlayerRigidBody, PlayerMoveSpeed, PlayerRotationSpeed);
    }

    private void RegisterEvents()
    {
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyMovement;
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyRotation;
        PlayerInput.Events.OnLookDirection += ApplyRotation;
        PlayerInput.Events.OnLookPoint += LookAtPoint;
    }
    
    private void ApplyMovement()
    {
        CharacterMovement.ApplyMovement(PlayerInput.MovementVector);
    }
    
    private void LookAtPoint(Vector3 point)
    {
        CharacterMovement.SetLookPoint(point);
    }
    
    private void ApplyRotation()
    {
        CharacterMovement.Rotate();
    }

    private void ApplyRotation(Vector2 direction)
    {
        CharacterMovement.LookInDirection(direction);
    }
}
