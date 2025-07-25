using UnityEngine;

public class ShelfButtons : MonoBehaviour
{
    public RectTransform panelToSlide;
    public SlideDirection slideFrom = SlideDirection.Left;

    public float slideDistance = 500f;
    public float slideSpeed = 5f;     

    private bool isVisible = false;
    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;
    private bool isSliding = false;

    public enum SlideDirection
    {
        Left,
        Right
    }

    void Start()
    {
        if (panelToSlide == null)
        {
            Debug.LogError("Assign a UI panel (RectTransform) to slide.");
            return;
        }

        Vector2 basePos = panelToSlide.anchoredPosition;
        float dir = (slideFrom == SlideDirection.Left) ? -1 : 1;

        hiddenPosition = basePos + new Vector2(dir * slideDistance, 0);
        visiblePosition = basePos;

     
        panelToSlide.anchoredPosition = hiddenPosition;
    }

    void OnMouseDown()
    {
        if (!isSliding)
            StartCoroutine(SlidePanel());
    }

    System.Collections.IEnumerator SlidePanel()
    {
        isSliding = true;

        Vector2 targetPos = isVisible ? hiddenPosition : visiblePosition;

        while (Vector2.Distance(panelToSlide.anchoredPosition, targetPos) > 0.1f)
        {
            panelToSlide.anchoredPosition = Vector2.Lerp(panelToSlide.anchoredPosition, targetPos, Time.deltaTime * slideSpeed);
            yield return null;
        }

        panelToSlide.anchoredPosition = targetPos;
        isVisible = !isVisible;
        isSliding = false;
    }
}

