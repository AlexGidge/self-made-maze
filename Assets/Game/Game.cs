using System;
using UnityEditor;
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

    internal void RoomEntered()
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
    public Action<Guid> OnNPCDied;

    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }

    public void NPCDied(Guid characterID)
    {
        OnNPCDied.Invoke(characterID);
    }
}

public enum GameStateType
{
    Playing = 1,
    Success = 2,
    Dead = 3,
}


#if UNITY_EDITOR
[CustomEditor(typeof(Game))]
public class Game_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        var game = (Game) target;
        if (game == null)
            return;
        
        if (GUILayout.Button("Room Entered"))
        {
            game.RoomEntered();
        }
        base.OnInspectorGUI();
    }
}
#endif
