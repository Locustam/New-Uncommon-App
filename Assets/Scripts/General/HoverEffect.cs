using UnityEngine;
using UnityEngine.EventSystems; // Required for event handling

[RequireComponent(typeof(EventTrigger))]
public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float enlargeAmount = 1.2f; // The amount by which the image enlarges on hover
    private Vector3 originalScale; // To store the original scale of the image
    private bool isHovering = false; // To track the hover state

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // If the mouse is hovering, ensure the object remains at the enlarged scale
        if (isHovering)
        {
            transform.localScale = originalScale * enlargeAmount;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true; // Set the flag to true when the mouse enters
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false; // Reset the flag when the mouse exits
        transform.localScale = originalScale; // Also reset the scale
    }
}
