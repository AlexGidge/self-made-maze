using System;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Current;
    
    public TriggerEvents TriggerEvents;

    private void Awake()
    {
        Current = this;
        TriggerEvents = new TriggerEvents();
    }
}

public sealed class TriggerEvents
{
    public Action OnRoomEntered;

    public void RoomEntered()
    {
        OnRoomEntered?.Invoke();
    }
}
