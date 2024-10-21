using UnityEngine;
using UnityEngine.UI;

public class SliderCopy : MonoBehaviour
{
    public Slider targetSlider;  // Reference to the target slider to copy from
    private Slider ownSlider;    // The slider component on this GameObject
    private Image targetFillImage;  // The fill image of the target slider
    private Image ownFillImage;     // The fill image of the own slider
    public bool CopyColor;

    void Start()
    {
        // Get the Slider component from this GameObject
        ownSlider = GetComponent<Slider>();
        if (ownSlider == null)
        {
            Debug.LogError("Slider component not found on the GameObject!");
            return;
        }

        if (targetSlider == null)
        {
            Debug.LogError("Target slider not assigned!");
            return;
        }

        // Get the fill images from both sliders
        targetFillImage = targetSlider.fillRect.GetComponent<Image>();
        ownFillImage = ownSlider.fillRect.GetComponent<Image>();

        if (targetFillImage == null || ownFillImage == null)
        {
            Debug.LogError("Fill Image not found on one of the sliders!");
            return;
        }

        // Copy initial properties from the target slider to this slider
        InitializeSliderProperties();
    }

    void Update()
    {
        // Continuously update the value and maxValue to keep them in sync
        if (ownSlider != null && targetSlider != null)
        {
            ownSlider.value = targetSlider.value;
            ownSlider.maxValue = targetSlider.maxValue;

            // Continuously copy the color of the target fill
            if (CopyColor)
            {
                ownFillImage.color = targetFillImage.color;
            }
        }
    }

    private void InitializeSliderProperties()
    {
        ownSlider.minValue = targetSlider.minValue;
        ownSlider.maxValue = targetSlider.maxValue;
        ownSlider.wholeNumbers = targetSlider.wholeNumbers;

        if (CopyColor)
        {
            // Copy the fill color initially
            ownFillImage.color = targetFillImage.color;
        }
    }

    // Optionally add a method to update all properties manually if needed
    public void UpdateSliderProperties()
    {
        if (targetSlider != null && ownSlider != null)
        {
            InitializeSliderProperties();
            ownSlider.value = targetSlider.value; // Ensure value is also updated
        }
    }
}
