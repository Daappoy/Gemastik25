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

    public Sprite ButtonOffSprite;
    public Sprite ButtonOnSprite;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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
            //sprite change
            SpriteRenderer spriteRenderer = ButtonOnSprite != null ? GetComponent<SpriteRenderer>() : null;
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = ButtonOnSprite;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Interactable") && DoorHold != null)
        {
            isPressed = false;
            ButtonOff?.Invoke();
            //sprite change
            SpriteRenderer spriteRenderer = ButtonOffSprite != null ? GetComponent<SpriteRenderer>() : null;
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = ButtonOffSprite;
            }
        }
    }
}
