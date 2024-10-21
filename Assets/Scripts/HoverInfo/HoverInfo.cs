using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Hoverinfo : MonoBehaviour
{
    public string hoverText;          // The text that will be displayed
    public GameObject hoverPrefab;    // The prefab to instantiate
    public string hoverInfoID;        // Unique ID for the hover info (optional)
    public float hoverDelay = 1.0f;   // Time in seconds required to hover before showing the hover UI

    private GameObject hoverUI;       // Reference to the instantiated hover UI
    private Canvas canvas;            // Canvas for proper positioning
    private RectTransform canvasRectTransform;

    private bool isHovering = false;  // Track whether the mouse is hovering
    private float hoverTimer = 0f;    // Timer to track hover time
    public Vector2 offset = new Vector2(10, -10); // Offset to avoid blocking original UI element

    void Start()
    {
        // Find the canvas in the scene (assuming the hover UI will be on the same canvas as other UI elements)
        canvas = FindObjectOfType<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
    }

    private void OnValidate()
    {
        // Check for Image component and generate one if it doesn't exist
        EnsureImageComponent();
    }

    void Update()
    {
        PerformHoverDetection();
    }

    // Ensures the GameObject has an Image component, creates one if it doesn't exist
    private void EnsureImageComponent()
    {
        Image image = GetComponent<Image>();

        if (image == null)
        {
            // Add an Image component if it's missing
            image = gameObject.AddComponent<Image>();

            // Set the alpha of the Image to 1 out of 255
            Color color = image.color;
            color.a = 1f / 255f;  // Alpha = 1 out of 255
            image.color = color;
        }
    }

    private void PerformHoverDetection()
    {
        // Perform a custom raycast to check if the mouse is over this UI element
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // List to store the raycast results
        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        // Check if this object is under the mouse pointer
        bool isMouseOver = false;
        foreach (var result in raycastResults)
        {
            if (result.gameObject == gameObject)
            {
                isMouseOver = true;
                break;
            }
        }

        // Handle hover state with delay
        if (isMouseOver)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= hoverDelay && !isHovering)
            {
                isHovering = true;
                CreateHoverUI();
            }
        }
        else
        {
            hoverTimer = 0f;  // Reset timer when mouse is not hovering
            if (isHovering)
            {
                isHovering = false;
                Destroy(hoverUI);
            }
        }
    }

    // Instantiate the hover UI prefab
    private void CreateHoverUI()
    {
        if (hoverPrefab == null)
        {
            Debug.LogError("Hover prefab is not assigned!");
            return;
        }

        // Instantiate the hover info prefab
        hoverUI = Instantiate(hoverPrefab, canvas.transform, false);

        // Find the TextMeshPro component in the instantiated prefab and set the text
        TextMeshProUGUI hoverUIText = hoverUI.GetComponentInChildren<TextMeshProUGUI>();
        if (hoverUIText != null)
        {
            hoverUIText.text = hoverText;
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI found in the hover prefab!");
        }

        // Adjust the position and size of the hover UI
        AdjustPosition();
    }

    // Adjust the position of the hover UI based on the position of the hovered UI element
    private void AdjustPosition()
    {
        RectTransform hoverRectTransform = hoverUI.GetComponent<RectTransform>();

        // Get the world corners of the hovered UI element
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);

        // Get the position of the top-right corner of the hovered UI element
        Vector3 worldPosition = corners[2]; // Top-right corner

        // Convert the world position to the canvas's local position
        Vector2 localPoint;

        // Make sure we are passing the correct camera for non-Overlay canvases
        Camera currentCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            RectTransformUtility.WorldToScreenPoint(currentCamera, worldPosition),
            currentCamera,
            out localPoint
        );

        // Apply the offset and set the local position of the hover UI
        localPoint += offset;

        hoverRectTransform.anchoredPosition = localPoint;

        // Optionally, clamp to screen
        ClampToScreen(hoverRectTransform);
    }

    // Ensure the hover UI remains within the screen bounds
    private void ClampToScreen(RectTransform hoverRectTransform)
    {
        // Get the world corners of the hover UI
        Vector3[] hoverCorners = new Vector3[4];
        hoverRectTransform.GetWorldCorners(hoverCorners);

        // Calculate the offset needed to keep the hover UI within the screen
        Vector3 offset = Vector3.zero;

        // Get screen boundaries in world coordinates
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

        // Left boundary check
        if (hoverCorners[0].x < 0)
            offset.x = -hoverCorners[0].x;

        // Right boundary check
        if (hoverCorners[2].x > Screen.width)
            offset.x = Screen.width - hoverCorners[2].x;

        // Bottom boundary check
        if (hoverCorners[0].y < 0)
            offset.y = -hoverCorners[0].y;

        // Top boundary check
        if (hoverCorners[1].y > Screen.height)
            offset.y = Screen.height - hoverCorners[1].y;

        // Apply the final position with the calculated offset
        hoverRectTransform.position += offset;
    }
}
