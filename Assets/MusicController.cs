using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource MusicSource;
    public AudioClip CalmTrack;
    public AudioClip CombatTrack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToCombatTrack()
    {
        MusicSource.clip = CombatTrack;
        MusicSource.Play();
    }

}
