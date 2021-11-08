using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D CharacterBody;
    public float MoveSpeed;
    public float RotationSpeed;
    public float DashSpeed;
    public float DashDelay;

    public AudioSource DashAudio;
    public ParticleSystem DashParticles;

    private float lastDash;
    public GameObject TargetObject { get; set; }
    public Quaternion TargetRotation { get; set; }

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
            DashAudio.PlayOneShot(DashAudio.clip);
            DashParticles.Play();
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
}