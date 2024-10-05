using UnityEngine;

public class CustomVerticalLayoutGroup : MonoBehaviour
{
    public float spacing = 10f; // The spacing between elements

    void Start()
    {
        AdjustLayout();
    }

    void Update()
    {
        AdjustLayout();

    }

    void AdjustLayout()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float totalHeight = 0f;
        float yOffset = 0f;

        // Calculate total height and initial yOffset
        for (int i = 0; i < rectTransform.childCount; i++)
        {
            RectTransform child = rectTransform.GetChild(i) as RectTransform;
            if (!child.gameObject.activeSelf) continue; // Skip inactive children

            totalHeight += child.sizeDelta.y;
            totalHeight += spacing; // Add spacing
        }

        yOffset = totalHeight / 2; // Start from the top

        // Adjust each child's position
        for (int i = 0; i < rectTransform.childCount; i++)
        {
            RectTransform child = rectTransform.GetChild(i) as RectTransform;
            if (!child.gameObject.activeSelf) continue; // Skip inactive children

            yOffset -= child.sizeDelta.y / 2; // Move up by half the child's height
            child.anchoredPosition = new Vector2(child.anchoredPosition.x, -yOffset + totalHeight / 2 - spacing / 2);
            yOffset -= (child.sizeDelta.y / 2 + spacing); // Move up for the next child
        }
    }
}
