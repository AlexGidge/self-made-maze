using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public CharacterMovement CharacterMovement;

    public AudioSource DashAudio;
    public ParticleSystem DashParticles;

    public Rigidbody2D PlayerRigidBody;
    public float PlayerMoveSpeed;
    public float PlayerRotationSpeed;
    
    public float PlayerDashDelay;
    public float PlayerDashSpeed;

    [SerializeField] internal Vector2 currentMovement;
    [SerializeField] internal Quaternion currentRotation => transform.rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        RegisterEvents();
        CharacterMovement.Initialise(PlayerRigidBody, PlayerMoveSpeed, PlayerRotationSpeed, PlayerDashSpeed, PlayerDashDelay, DashAudio, DashParticles);
    }

    private void RegisterEvents()
    {
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyMovement;
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyRotation;
        PlayerInput.Events.OnLookDirection += ApplyRotation;
        PlayerInput.Events.OnLookPoint += LookAtPoint;
        PlayerInput.Events.OnDash += Dash;
    }
    
    private void ApplyMovement()
    {
        CharacterMovement.ApplyMovement(PlayerInput.MovementVector);
    }
    
    private void Dash()
    {
        CharacterMovement.Dash(PlayerInput.MovementVector);
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
