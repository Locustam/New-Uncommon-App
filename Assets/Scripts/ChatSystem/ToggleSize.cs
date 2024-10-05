using UnityEngine;
using UnityEngine.UI;

public class ToggleSize: MonoBehaviour
{
    public Image image;
    private Vector3 originalSize;
    private Vector3 originalPosition;
    public float horizontalPadding = 50f; // Horizontal shift amount
    public float verticalPadding = 50f;   // Vertical shift amount
    private bool isEnlarged = false;

    private GameObject backgroundImage;

    public float enlargeScale = 2;
    void Start()
    {
        // Get the Image component
        if (image == null)
        {
            Debug.LogError("ToggleSizeAndPosition script requires an Image component on the same GameObject.");
            return;
        }

        backgroundImage = GameObject.Find("ChatMessageImageBackground");

        // Store the original size and position of the image
        originalSize = image.rectTransform.localScale;
        originalPosition = image.rectTransform.localPosition;
    }

    public void ToggleImageSizeAndPosition()
    {
        // if (image == null) return;

        // if (!isEnlarged)
        // {
        //     // Enlarge the image and apply padding
        //     backgroundImage.GetComponent<Image>().enabled = true;
        //     image.rectTransform.localScale = originalSize * enlargeScale;
        //     image.rectTransform.localPosition = new Vector3(
        //         originalPosition.x + horizontalPadding, 
        //         originalPosition.y + verticalPadding, 
        //         originalPosition.z);
        // }
        // else
        // {
        //     backgroundImage.GetComponent<Image>().enabled = false;
        //     // Return the image to its original size and position
        //     image.rectTransform.localScale = originalSize;
        //     image.rectTransform.localPosition = originalPosition;
        // }

        // // Toggle the state
        // isEnlarged = !isEnlarged;
        ImageDisplayer.Instance.ToggleImageDisplayer(image.sprite);
    }
}
