
using UnityEngine;


public class Router : MonoBehaviour
{
    public int RouterHID;
    public bool isPressed = false;
    public DoorHold DoorHold;
    public Sprite ButtonOffSprite;
    public Sprite ButtonOnSprite;
    public Animator RouterAnimator;
    public AudioManager audioManager;

    void Start()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
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
        int playerLayer = LayerMask.NameToLayer("Small");
        if (collision.gameObject.layer == playerLayer && DoorHold != null && !isPressed)
        {
            audioManager.PlaySFX(audioManager.RouterSound);
            RouterAnimator.SetTrigger("connected");
            isPressed = true;
            SpriteRenderer spriteRenderer = ButtonOnSprite != null ? GetComponent<SpriteRenderer>() : null;
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = ButtonOnSprite;
            }

        }
    }
    
}
