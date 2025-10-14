
using UnityEngine;

public class TargetPointScript : MonoBehaviour
{
    public PauseMenu pauseMenu; 
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
        if (playersInZone == 3 && !gameFinished)
        {
            Debug.Log("Three players are in the zone!");
            gameFinished = true;
            pauseMenu.GameFinised(); 
        }
    }
}
