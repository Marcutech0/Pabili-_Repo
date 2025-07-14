using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private RectTransform rectTransform;
    private Image image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //image.color = new Color32(255, 255, 255, 170);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //image.color = new Color32(255, 255, 255, 225);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
    }
}
