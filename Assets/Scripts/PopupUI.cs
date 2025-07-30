using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public CharSwitch CharSwitchScript;
    public RectTransform popupPanel;
    public float slideDuration = 0.5f;
    public float stayDuration = 2.5f;
    private Vector2 offscreenPos;
    private Vector2 onscreenPos;

    [Header("Character Selection Carousel")]
    public Image[] characterIcons; // Array of 3 character icons
    public RectTransform leftPosition;   // Position for left character
    public RectTransform centerPosition; // Position for center (selected) character
    public RectTransform rightPosition;  // Position for right character
    public float iconTransitionSpeed = 5f; // Speed of icon movement

    [SerializeField]
    private bool isPopupVisible = false;

    void Start()
    {
        onscreenPos = popupPanel.anchoredPosition;
        offscreenPos = new Vector2(onscreenPos.x, onscreenPos.y + popupPanel.rect.height + 20); // adjust 20 as buffer

        // Start offscreen
        popupPanel.anchoredPosition = offscreenPos;

        // Initialize character carousel
        UpdateCharacterCarousel();

        // Example trigger
        // ShowPopup();
    }

    void Update()
    {


        // Example trigger for showing the popup
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) 
        {
            ShowPopup();
        }

        // Update carousel if popup is visible and character index changes
        if (isPopupVisible)
        {
            UpdateCharacterCarousel();
        }
    }

    void UpdateCharacterCarousel()
    {
        if (characterIcons == null || characterIcons.Length != 3) return;
        if (CharSwitchScript == null) return;

        int currentIndex = CharSwitchScript.characterIndex;
        
        // Calculate positions for each character based on current selection
        for (int i = 0; i < 3; i++)
        {
            Vector2 targetPosition;
            float targetScale;
            float targetAlpha;

            if (i == currentIndex)
            {
                // Selected character goes to center
                targetPosition = centerPosition.anchoredPosition;
                targetScale = 0.1f; // Slightly larger for selected character
                targetAlpha = 1f;
            }
            else if (i == (currentIndex - 1 + 3) % 3) // Character to the left of selected (with wraparound)
            {
                targetPosition = leftPosition.anchoredPosition;
                targetScale = 0.09f; // Smaller for non-selected
                targetAlpha = 0.6f;
            }
            else // Character to the right of selected
            {
                targetPosition = rightPosition.anchoredPosition;
                targetScale = 0.09f; // Smaller for non-selected
                targetAlpha = 0.6f;
            }

            // Smoothly move icons to their target positions
            RectTransform iconRect = characterIcons[i].rectTransform;
            iconRect.anchoredPosition = Vector2.Lerp(iconRect.anchoredPosition, targetPosition, Time.deltaTime * iconTransitionSpeed);
            iconRect.localScale = Vector3.Lerp(iconRect.localScale, Vector3.one * targetScale, Time.deltaTime * iconTransitionSpeed);

            // Adjust alpha for non-selected characters
            Color iconColor = characterIcons[i].color;
            iconColor.a = Mathf.Lerp(iconColor.a, targetAlpha, Time.deltaTime * iconTransitionSpeed);
            characterIcons[i].color = iconColor;
        }
    }

    public void ShowPopup()
    {
        if (isPopupVisible)
        {
            // If popup is already visible, just restart the stay timer without sliding in again
            StopAllCoroutines();
            StartCoroutine(ExtendStayTime());
        }
        else if (!isPopupVisible)
        {
            // If popup is not visible, start normally
            StartCoroutine(SlideInOut());
        }
    }

    System.Collections.IEnumerator ExtendStayTime()
    {
        // Make sure popup is in the correct position and visible
        popupPanel.anchoredPosition = onscreenPos;
        isPopupVisible = true;

        // Wait for the stay duration
        yield return new WaitForSeconds(stayDuration);

        // Then slide out
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / slideDuration;
            popupPanel.anchoredPosition = Vector2.Lerp(onscreenPos, offscreenPos, t);
            yield return null;
        }

        isPopupVisible = false;
    }

    System.Collections.IEnumerator SlideInOut()
    {
        isPopupVisible = true;

        // Slide in
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / slideDuration;
            popupPanel.anchoredPosition = Vector2.Lerp(offscreenPos, onscreenPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(stayDuration);

        // Slide out
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / slideDuration;
            popupPanel.anchoredPosition = Vector2.Lerp(onscreenPos, offscreenPos, t);
            yield return null;
        }

        isPopupVisible = false;
    }
    

}
