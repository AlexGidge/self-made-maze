using Cinemachine;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D CharacterBody;
    private float MoveSpeed;
    private float RotationSpeed;
    private float DashSpeed;
    private float DashDelay;
    private bool initialised;
    
    
    private float lastDash;
    public GameObject TargetObject { get; set; }
    public Quaternion TargetRotation { get; set; }

    public void Initialise(Rigidbody2D rigidBody, float moveSpeed, float rotationSpeed, float dashSpeed, float dashDelay)
    {
        if (!initialised)
        {
            CharacterBody = rigidBody;
            MoveSpeed = moveSpeed;
            RotationSpeed = rotationSpeed;
            DashSpeed = dashSpeed;
            DashDelay = dashDelay;
            initialised = true;
        }
    }
    
    public bool HasActiveTarget()
    {
        return (TargetObject != null && TargetObject.activeSelf);
    }

    public void ApplyMovement(Vector2 movement)
    {
        CharacterBody.AddForce(movement * MoveSpeed);
    }

    public void Dash(Vector2 movement)
    {
        if (lastDash + DashDelay < Time.time)
        {
            lastDash = Time.time;
            CharacterBody.AddForce(movement * DashSpeed);
        }
    }

    public void LookInDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            TargetRotation = VectorHelper.Calculate2DLookRotation(new Vector3(0, 0, 0), direction);
        }
    }

    public void SetLookPoint(Vector3 point)
    {
        TargetRotation = VectorHelper.Calculate2DLookRotation(transform.position, point);
    }
    
    public void Rotate()
    {
        if (1 - Mathf.Abs(Quaternion.Dot(transform.rotation, TargetRotation)) < 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, RotationSpeed);
        }
    }

    public void SetLookTarget(GameObject go)
    {
        TargetObject = go;
    }

    public void SetRotationForTarget()
    {
        if (HasActiveTarget())
        {
            TargetRotation = VectorHelper.Calculate2DLookRotation(transform.position, TargetObject.transform.position);
        }
    }
}
