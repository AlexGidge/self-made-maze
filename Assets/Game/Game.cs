using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Current;
    
    public GameEvents GameEvents;

    public AudioSource RoomEnteredAudio;
    public AudioSource LevelCompletedAudio;
    public GameStateType GameState { get; set; }

    private void Awake()
    {
        Current = this;
        GameState = GameStateType.Playing;
        GameEvents = new GameEvents();
    }

    private void OnEnable()
    {
        TriggerManager.Current.TriggerEvents.OnRoomEntered += RoomEntered;
        GameEvents.OnLevelCompleted += LevelCompleted;
    }

    private void RoomEntered()
    {
        RoomEnteredAudio.Play();
    }
    
    private void LevelCompleted()
    {
        GameState = GameStateType.Success;
        LevelCompletedAudio.Play();
    }

    private void OnDisable()
    {
        TriggerManager.Current.TriggerEvents.OnRoomEntered -= RoomEntered;
    }
}

public class GameEvents
{
    public Action OnLevelCompleted;

    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
}

public enum GameStateType
{
    Playing = 1,
    Success = 2,
    Dead = 3,
}