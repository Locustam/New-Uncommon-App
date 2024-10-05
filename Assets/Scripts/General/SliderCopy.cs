using UnityEngine;
using UnityEngine.UI;

public class SliderCopy : MonoBehaviour
{
    public Slider targetSlider;  // Reference to the target slider to copy from
    private Slider ownSlider;    // The slider component on this GameObject

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

        // Copy initial properties from the target slider to this slider
        InitializeSliderProperties();
    }

    void Update()
    {
        // Continuously update only the value to keep them in sync
        if (ownSlider != null && targetSlider != null)
        {
            ownSlider.value = targetSlider.value;
        }
    }

    private void InitializeSliderProperties()
    {
        ownSlider.minValue = targetSlider.minValue;
        ownSlider.maxValue = targetSlider.maxValue;
        ownSlider.wholeNumbers = targetSlider.wholeNumbers;
        // Initialize other properties as necessary
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
