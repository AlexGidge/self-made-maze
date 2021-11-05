using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController current;

    public AudioSource MusicSource;
    public AudioClip OminousTrack;
    public AudioClip CombatTrack;
    public AudioClip SoftTrack;

    public float softVolume;

    // Start is called before the first frame update
    void OnEnable()
    {
        current = this;
        TriggerManager.Current.TriggerEvents.OnRoomEntered += RoomEntered;
        Game.Current.GameEvents.OnLevelCompleted += LevelCompleted;
    }

    private void LevelCompleted()
    {
        MusicSource.Stop();
        StartCoroutine("ChangeToSoftTrack");
    }

    private void OnDisable()
    {
        TriggerManager.Current.TriggerEvents.OnRoomEntered -= RoomEntered;
        Game.Current.GameEvents.OnLevelCompleted -= LevelCompleted;

    }

    private void RoomEntered()
    {
        MusicSource.Stop();
        StartCoroutine("ChangeToCombatTrack");
    }

    public IEnumerator ChangeToCombatTrack()
    {
        yield return new WaitForSeconds(4f);
        MusicSource.clip = CombatTrack;
        MusicSource.Play();
    }
    
    public IEnumerator ChangeToSoftTrack()
    {
        yield return new WaitForSeconds(2f);
        MusicSource.clip = SoftTrack;
        MusicSource.volume = softVolume;
        MusicSource.Play();
    }
}
