using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalScale; // Store the original scale

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale; // Save the original scale
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        rectTransform.localScale = originalScale * 1.1f; // Make the UI element a bit larger when dragged
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        rectTransform.localScale = originalScale; // Reset the scale back to original
    }
}
