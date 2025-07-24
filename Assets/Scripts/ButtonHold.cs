using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHold : MonoBehaviour
{
    public int ButtonHID;
    public bool isPressed = false;
    public DoorHold DoorHold;
    public UnityEvent ButtonOn;
    public UnityEvent ButtonOff;

    void Start()
    {
        DoorHold[] doors = FindObjectsOfType<DoorHold>();
        foreach (DoorHold door in doors)
        {
            if (door.DoorHID == ButtonHID)
            {
                DoorHold = door;
                break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Interactable") && DoorHold != null)
        {
            isPressed = true;
            ButtonOn?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Interactable") && DoorHold != null)
        {
            isPressed = false;
            ButtonOff?.Invoke();
        }
    }
}
