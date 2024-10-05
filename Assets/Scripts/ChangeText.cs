using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TMP_Text displayText; 

    public void ChangeTextVis(string newText)
    {
        if (displayText != null)
        {
            displayText.text = newText;
        }
        else
        {
            Debug.LogWarning("Display Text component not assigned!");
        }
    }
}
