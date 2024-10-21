using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeritScholarshipController : MonoBehaviour
{
    public Slider scholarshipSlider;  // Reference to the Slider component
    public float minValue = 0f;       // The minimum value the slider should be clamped to
    public float maxValue = 100f;     // The maximum value for the slider
    public TextMeshProUGUI financeText;

    void Start()
    {
        // Ensure the slider's maxValue is set and the value is clamped initially
        InitializeSlider();
    }

    // Initializes the slider with the max value and ensures the value is clamped
    public void InitializeSlider()
    {
        if (scholarshipSlider == null)
        {
            Debug.LogError("Slider is not assigned!");
            return;
        }

        scholarshipSlider.maxValue = maxValue;
        scholarshipSlider.value = Mathf.Clamp(minValue, minValue, maxValue);  // Clamp the initial value to be within bounds
        financeText.text = StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship.ToString() + " (" + StudentAdmissionManager.Instance.studentInfo.data._finance.ToString() + " + " + (StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship - StudentAdmissionManager.Instance.studentInfo.data._finance).ToString() + " scholarships)";

    }

    // Adds value to the slider while clamping to the max value and ensuring it doesn't go below the min value
    public void AddValue(float amount)
    {
        if (scholarshipSlider == null) return;
        if (StudentAdmissionManager.Instance.totalScholarship >= scholarshipSlider.value + amount - minValue)
        {
            float newValue = scholarshipSlider.value + amount;
            scholarshipSlider.value = Mathf.Clamp(newValue, minValue, maxValue);
            StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship = (int)newValue;
            financeText.text = StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship.ToString() + " (" + StudentAdmissionManager.Instance.studentInfo.data._finance.ToString() + " + " + (StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship - StudentAdmissionManager.Instance.studentInfo.data._finance).ToString() + " scholarships)";

        }
    }

    // Removes value from the slider while ensuring it doesn't go below the min value
    public void RemoveValue(float amount)
    {
        if (scholarshipSlider == null) return;

        float newValue = scholarshipSlider.value - amount;
        scholarshipSlider.value = Mathf.Clamp(newValue, minValue, maxValue);
        StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship = (int)newValue;
        financeText.text = StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship.ToString() + " (" + StudentAdmissionManager.Instance.studentInfo.data._finance.ToString() + " + " + (StudentAdmissionManager.Instance.studentInfo.data._financeWithScholarship - StudentAdmissionManager.Instance.studentInfo.data._finance).ToString() + " scholarships)";

    }
}
