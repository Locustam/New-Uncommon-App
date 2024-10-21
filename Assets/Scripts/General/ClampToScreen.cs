using UnityEngine;

public class ClampToScreen : MonoBehaviour
{
    private RectTransform rectTransform;  // The RectTransform of the UI element
    private Canvas canvas;  // The parent Canvas

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // Make sure the UI is clamped to screen on start
        Clamp();
    }

    void Update()
    {
        // Continuously clamp the UI in case of movement or window resizing
        Clamp();
    }

    public void Clamp()
    {
        // Get the world corners of the RectTransform
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // Calculate offsets for clamping the UI element within the screen
        Vector3 offset = Vector3.zero;

        // Get screen boundaries in world coordinates
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

        // Left boundary check
        if (corners[0].x < 0)
            offset.x = -corners[0].x;

        // Right boundary check
        if (corners[2].x > Screen.width)
            offset.x = Screen.width - corners[2].x;

        // Bottom boundary check
        if (corners[0].y < 0)
            offset.y = -corners[0].y;

        // Top boundary check
        if (corners[1].y > Screen.height)
            offset.y = Screen.height - corners[1].y;

        // Apply the offset to clamp the position within the screen
        rectTransform.position += offset;
    }
}
