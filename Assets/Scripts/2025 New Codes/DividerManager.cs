using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DividerManager : MonoBehaviour
{
    public Button[] buttons;
    public Sprite activeSprite; // Default active sprite
    public Sprite inactiveSprite; // Default inactive prite
    public RectTransform[] pages;
    public float[] buttonOffsets;

    [SerializeField] Button activeButton;
    // Start is called before the first frame update
    void Start()
{
    foreach (Button button in buttons)
    {
        // Add listener to each button
        button.onClick.AddListener(() => OnButtonClick(button));

        // Set each button to its inactive state initially, except for the first one
        if (button != buttons[0])
        {
            SetButtonSprite(button, inactiveSprite, false);
        }
    }

    // Show the first page by default and set its button to active
    if (buttons.Length > 0)
    {
        ShowPage(buttons[0]); // Show the first page by default
        SetActiveButton(buttons[0]); // Make the first button the active button
        activeButton = buttons[0]; // Update the activeButton reference
    }
}

    void OnButtonClick(Button clickedButton)
    {
        if (activeButton == clickedButton)
        {
            return; // Do nothing if the button is already active
        }
        // If there's an active button, reset its sprite
        if (activeButton != null && activeButton != clickedButton)
        {
            ResetButton(activeButton);
        }

        // Set the clicked button as active
        activeButton = clickedButton;
        SetActiveButton(clickedButton);
        ShowPage(clickedButton);
    }

    void SetActiveButton(Button button)
    {
        int index = System.Array.IndexOf(buttons, button);
        float offset = buttonOffsets[index];

        // Set the button to the active sprite
        SetButtonSprite(button, activeSprite, true);

        RectTransform rect = button.GetComponent<RectTransform>();
        rect.anchoredPosition += new Vector2(0, offset);
    }

    void ResetButton(Button button)
    {
        int index = System.Array.IndexOf(buttons, button);
        float offset = buttonOffsets[index];

        // Set the button to the inactive sprite
        SetButtonSprite(button, inactiveSprite, false);

        RectTransform rect = button.GetComponent<RectTransform>();
        rect.anchoredPosition -= new Vector2(0, offset);
    }


    void SetButtonSprite(Button button, Sprite defaultSprite, bool isActive)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            // Get the ButtonData component from the button
            ButtonData buttonData = button.GetComponent<ButtonData>();

            // Use the appropriate sprite based on the active state
            if (buttonData != null)
            {
                buttonImage.sprite = isActive
                    ? (buttonData.customActiveSprite != null ? buttonData.customActiveSprite : defaultSprite)
                    : (buttonData.customInactiveSprite != null ? buttonData.customInactiveSprite : defaultSprite);
            }
            else
            {
                buttonImage.sprite = defaultSprite; // Fallback to default if no ButtonData is found
            }
        }
    }





    void ShowPage(Button button)
    {
        int index = System.Array.IndexOf(buttons, button);

        // Activate the corresponding page
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null)
            {
                if (i == index)
                {
                    pages[i].gameObject.SetActive(true);
                }
                else
                {
                    pages[i].gameObject.SetActive(false);
                }
            }        
        }
    }
}
