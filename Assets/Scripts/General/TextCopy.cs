using UnityEngine;
using TMPro; // Import the TMPro namespace to access TextMeshPro components

public class TextCopy : MonoBehaviour
{
    public TextMeshProUGUI targetText;  // Reference to the target TextMeshProUGUI to copy from
    private TextMeshProUGUI ownText;    // The TextMeshProUGUI component on this GameObject

    public bool copyColor;
    void Start()
    {
        // Get the TextMeshProUGUI component from this GameObject
        ownText = GetComponent<TextMeshProUGUI>();
        if (ownText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the GameObject!");
            return;
        }

        if (targetText == null)
        {
            Debug.LogError("Target TextMeshProUGUI not assigned!");
            return;
        }

        // Initialize text properties from the target text to this text
        InitializeTextProperties();
    }

    void Update()
    {
        // // Continuously update the text and other properties to keep them in sync
        // if (ownText != null && targetText != null)
        // {
        //     ownText.text = targetText.text;
        //     UpdateTextProperties(); // Update other properties if they are dynamic
        // }

        ownText.text = targetText.text;
    }

    private void InitializeTextProperties()
    {
        // Copy all necessary initial properties from the target TextMeshProUGUI
        ownText.fontSize = targetText.fontSize;
        if (copyColor)
        {
            ownText.color = targetText.color;
        }
        ownText.alignment = targetText.alignment;
        ownText.font = targetText.font;
        ownText.fontStyle = targetText.fontStyle;
    }

    private void UpdateTextProperties()
    {
        // Continuously update these properties if they are expected to change
        ownText.fontSize = targetText.fontSize;
        if (copyColor)
        {
            ownText.color = targetText.color;
        }
        ownText.alignment = targetText.alignment;
        // Add more properties if needed
    }

    // Optionally add a method to update all text properties manually if needed
    public void UpdateAllTextProperties()
    {
        if (targetText != null && ownText != null)
        {
            InitializeTextProperties();
            ownText.text = targetText.text;
        }
    }
}
