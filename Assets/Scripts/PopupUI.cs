using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public RectTransform popupPanel;
    public float slideDuration = 0.5f;
    public float stayDuration = 2.5f;
    private Vector2 offscreenPos;
    private Vector2 onscreenPos;

    [SerializeField]
    private bool isPopupVisible = false;

    void Start()
    {
        onscreenPos = popupPanel.anchoredPosition;
        offscreenPos = new Vector2(onscreenPos.x, onscreenPos.y + popupPanel.rect.height + 20); // adjust 20 as buffer

        // Start offscreen
        popupPanel.anchoredPosition = offscreenPos;

        // Example trigger
        // ShowPopup();
    }

    void Update()
    {
        // Example trigger for showing the popup
        if (Input.GetKeyDown(KeyCode.P)) // Press 'P' to show the popup
        {
            ShowPopup();
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
