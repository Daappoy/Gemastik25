using UnityEngine;
using UnityEngine.InputSystem;


public class ElectricBox : MonoBehaviour
{
    public int ElectricBoxHID;
    public bool isPressed = false;
    public bool canInteract = false;
    public DoorHold DoorHold;
    public Sprite FixedSprite;
    public GameObject FKeyText;
    public AudioManager audioManager;

    void Start()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
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
            audioManager.PlaySFX(audioManager.ElectricBoxSound);
            isPressed = true;
            canInteract = false;
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
