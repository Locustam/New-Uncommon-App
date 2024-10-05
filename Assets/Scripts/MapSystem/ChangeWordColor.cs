using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ChangeWordColor : MonoBehaviour 
{
    public TextMeshProUGUI textMeshPro;
    public Color normalColor;
    public Color pressedColor;

    private Button button;

    void Start()
    {
        // Get the Button component attached to the same GameObject
        button = GetComponent<Button>();

    }

    private void Update()
    {
        button.onClick.Invoke();
    }
    // Method to handle button click event
    private void OnClick()
    {
        textMeshPro.color = pressedColor;
    }

    // Method to handle button pressed state
    private void OnPressed(PointerEventData eventData)
    {
        textMeshPro.color = pressedColor;
    }

    // Method to handle button released state
    private void OnReleased(PointerEventData eventData)
    {
        textMeshPro.color = normalColor;
    }
}
