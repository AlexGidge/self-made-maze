using System;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    public static EngineManager Current { get; private set; }
    public EngineEvents Events;

    private void Awake()
    {
        Events = new EngineEvents();
        Current = this;
    }

    private void Update()
    {
        Events.Update();
    }

    private void FixedUpdate()
    {
        Events.FixedUpdate();
    }
}

public sealed class EngineEvents
{
    public Action EveryUpdate;
    public Action Every5Updates;
    public Action EveryPhysicsUpdate;
    public Action EveryInputUpdate;

    public void Update()
    {
        EveryUpdate?.Invoke();
        EveryInputUpdate?.Invoke();
        if (Time.frameCount % 5 == 0)
            Every5Updates?.Invoke();
    }

    public void FixedUpdate()
    {
        EveryPhysicsUpdate?.Invoke();
    }
}