using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Router : MonoBehaviour
{
    public int RouterHID;
    public bool isPressed = false;
    public DoorHold DoorHold;
    public UnityEvent ButtonOn;
    public UnityEvent ButtonOff;

    public Sprite ButtonOffSprite;
    public Sprite ButtonOnSprite;
    public Animator RouterAnimator;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        DoorHold[] doors = FindObjectsOfType<DoorHold>();
        foreach (DoorHold door in doors)
        {
            if (door.DoorHID == RouterHID)
            {
                DoorHold = door;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Replace "Player" with the desired layer name, e.g., "Player"
        int playerLayer = LayerMask.NameToLayer("Small");
        if (collision.gameObject.layer == playerLayer && DoorHold != null)
        {
            RouterAnimator.SetTrigger("connected");
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
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     int playerLayer = LayerMask.NameToLayer("Small");
    //     if (collision.gameObject.layer == playerLayer && DoorHold != null)
    //     {
    //         isPressed = false;
    //         ButtonOff?.Invoke();
    //         //sprite change
    //         SpriteRenderer spriteRenderer = ButtonOffSprite != null ? GetComponent<SpriteRenderer>() : null;
    //         if (spriteRenderer != null)
    //         {
    //             spriteRenderer.sprite = ButtonOffSprite;
    //         }
    //     }
    // }
}
