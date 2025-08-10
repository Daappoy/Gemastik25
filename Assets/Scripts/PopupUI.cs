using UnityEngine;

using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public CharSwitch CharSwitchScript;
    public RectTransform popupPanel;
    public float slideDuration = 0.5f;
    public float stayDuration = 3f;
    private Vector2 offscreenPos;
    private Vector2 onscreenPos;

    [Header("Character Selection Carousel")]
    public Image[] characterIcons;
    public RectTransform leftPosition;
    public RectTransform centerPosition;
    public RectTransform rightPosition;
    public float iconTransitionSpeed = 5f;

    [SerializeField]
    private bool isPopupVisible = false;

    void Start()
    {
        onscreenPos = popupPanel.anchoredPosition;
        offscreenPos = new Vector2(onscreenPos.x, onscreenPos.y + popupPanel.rect.height + 100); 
        popupPanel.anchoredPosition = offscreenPos;
        UpdateCharacterCarousel();
        ShowPopup();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) 
        {
            ShowPopup();
        }

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
        
        
        for (int i = 0; i < 3; i++)
        {
            Vector2 targetPosition;
            float targetScale;
            float targetAlpha;

            if (i == currentIndex)
            {
                targetPosition = centerPosition.anchoredPosition;
                targetScale = 0.3f;
                targetAlpha = 1f;
            }
            else if (i == (currentIndex - 1 + 3) % 3) 
            {
                targetPosition = leftPosition.anchoredPosition;
                targetScale = 0.25f;
                targetAlpha = 0.4f;
            }
            else 
            {
                targetPosition = rightPosition.anchoredPosition;
                targetScale = 0.25f; 
                targetAlpha = 0.4f;
            }

            // Smoothly move icons to their target positions
            RectTransform iconRect = characterIcons[i].rectTransform;
            iconRect.anchoredPosition = Vector2.Lerp(iconRect.anchoredPosition, targetPosition, Time.deltaTime * iconTransitionSpeed);
            iconRect.localScale = Vector3.Lerp(iconRect.localScale, Vector3.one * targetScale, Time.deltaTime * iconTransitionSpeed);

            Color iconColor = characterIcons[i].color;
            iconColor.a = Mathf.Lerp(iconColor.a, targetAlpha, Time.deltaTime * iconTransitionSpeed);
            characterIcons[i].color = iconColor;
        }
    }

    public void ShowPopup()
    {
        if (isPopupVisible)
        {
            StopAllCoroutines();
            StartCoroutine(ExtendStayTime());
        }
        else if (!isPopupVisible)
        {
            StartCoroutine(SlideInOut());
        }
    }

    System.Collections.IEnumerator ExtendStayTime()
    {
        popupPanel.anchoredPosition = onscreenPos;
        isPopupVisible = true;

        // extend
        yield return new WaitForSeconds(stayDuration);

        // baru ngumpet
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

        //muncul
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / slideDuration;
            popupPanel.anchoredPosition = Vector2.Lerp(offscreenPos, onscreenPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(stayDuration);

        // ngumpet
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
