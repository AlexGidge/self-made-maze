using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionsMaster InputMaster;
    private InputAction MovementAction;
    public InputEvents Events;
    public Camera PlayerCamera;

    public Vector2 MovementVector => InputMaster.Player.Move.ReadValue<Vector2>();
    public Vector2 PointScreenPosition => InputMaster.Player.PointLook.ReadValue<Vector2>();
    public Vector2 DirectionalLook => InputMaster.Player.DirectionalLook.ReadValue<Vector2>();
    
    void Awake()
    { 
        Events = new InputEvents();
        SetupInput();
    }

    private void SetupInput()
    {
        InputMaster = new InputActionsMaster();
        InputMaster.Enable();
        InputMaster.Player.Move.Enable();
        InputMaster.Player.DirectionalLook.Enable();
        InputMaster.Player.PointLook.Enable();

        InputMaster.Player.PointLook.performed += PointLookPerformed;
        InputMaster.Player.DirectionalLook.performed += DirectionLookPerformed;
    }

    private void DirectionLookPerformed(InputAction.CallbackContext obj)
    {
        Events.LookDirection(DirectionalLook);
    }

    private void PointLookPerformed(InputAction.CallbackContext obj)
    {
        Vector2 worldLocation = PlayerCamera.ScreenToWorldPoint(PointScreenPosition);
        Events.LookAtPoint(worldLocation);
    }
}

public sealed class InputEvents
{
    public Action<Vector2> OnLookDirection;
    public Action<Vector3> OnLookPoint;

    public void LookDirection(Vector2 direction)
    {
        OnLookDirection?.Invoke(direction);
    }
    
    public void LookAtPoint(Vector3 point)
    {
        OnLookPoint?.Invoke(point);
    }
}