using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointScript : MonoBehaviour
{
    public PauseMenu pauseMenu; // Reference to the PauseMenu script
    public int playersInZone = 0;
    private bool gameFinished = false;

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
        // Check if we have the target number of players and game hasn't finished yet
        if (playersInZone == 3 && !gameFinished)
        {
            // Do something when 3 players are in the zone
            Debug.Log("Three players are in the zone!");
            gameFinished = true; // Set flag to prevent multiple calls
            pauseMenu.GameFinised(); // Call the GameFinised method from PauseMenu
        }
    }
}
