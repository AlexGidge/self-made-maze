using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public AudioSource RoomEnteredAudio;
    
    private void OnEnable()
    {
        TriggerManager.Current.TriggerEvents.OnRoomEntered += RoomEntered;
    }

    private void RoomEntered()
    {
        RoomEnteredAudio.Play();
    }

    private void OnDisable()
    {
        TriggerManager.Current.TriggerEvents.OnRoomEntered -= RoomEntered;
    }
}
