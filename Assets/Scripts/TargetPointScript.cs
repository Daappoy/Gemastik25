using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointScript : MonoBehaviour
{
    public int playersInZone = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInZone++;
            Debug.Log("Player entered zone. Players in Zone: " + playersInZone);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInZone--;
            Debug.Log("Player left zone. Players in Zone: " + playersInZone);
        }
    }

    public void Update()
    {
        // Check if we have the target number of players
        if (playersInZone == 3)
        {
            // Do something when 3 players are in the zone
            Debug.Log("Three players are in the zone!");
            // Time.timeScale = 0f;
            // Note: Don't reset playersInZone here unless you want the action to happen only once
        }
    }
}
