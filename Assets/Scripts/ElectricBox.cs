using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ElectricBox : MonoBehaviour
{
    public int ElectricBoxHID;
    public bool isPressed = false;
    public bool canInteract = false;
    public DoorHold DoorHold;
    public Sprite FixedSprite;
    public GameObject FKeyText;

    void Start()
    {
        FKeyText.SetActive(false);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        DoorHold[] doors = FindObjectsOfType<DoorHold>();
        foreach (DoorHold door in doors)
        {
            if (door.DoorHID == ElectricBoxHID)
            {
                DoorHold = door;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Replace "Player" with the desired layer name, e.g., "Player"
        int playerLayer = LayerMask.NameToLayer("Medium");
        if (collision.gameObject.layer == playerLayer && DoorHold != null)
        {
            FKeyText.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Medium");
        if (collision.gameObject.layer == playerLayer && DoorHold != null)
        {
            FKeyText.SetActive(false);
            canInteract = false;
        }
    }

    public void OnFix(InputAction.CallbackContext context)
    {
        if (context.performed && canInteract)
        {
            isPressed = true;
            canInteract = false;
            //sprite change
            SpriteRenderer spriteRenderer = FixedSprite != null ? GetComponent<SpriteRenderer>() : null;
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = FixedSprite;
            }
            if (DoorHold != null)
            {
                DoorHold.isOpen = true;
            }
        }
    }
}
