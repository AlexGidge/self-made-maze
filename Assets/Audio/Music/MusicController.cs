using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController current;

    public AudioSource MusicSource;
    public AudioClip CalmTrack;
    public AudioClip CombatTrack;

    // Start is called before the first frame update
    void OnEnable()
    {
        current = this;
        TriggerManager.Current.TriggerEvents.OnRoomEntered += RoomEntered;
    }

    private void OnDisable()
    {
        TriggerManager.Current.TriggerEvents.OnRoomEntered -= RoomEntered;
    }

    private void RoomEntered()
    {
        MusicSource.Stop();
        StartCoroutine("ChangeToCombatTrack");
    }

    public IEnumerator ChangeToCombatTrack()
    {
        yield return new WaitForSeconds(6f);
        MusicSource.clip = CombatTrack;
        MusicSource.Play();
    }
}
