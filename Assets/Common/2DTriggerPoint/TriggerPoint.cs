using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    public int MaxTriggerEvents = 1;
    private int triggerCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerCount < MaxTriggerEvents)
        {
            if (other.gameObject.tag == "Player")
            {
                triggerCount++;
                TriggerManager.Current.TriggerEvents.RoomEntered();
            }
        }
    }
}
