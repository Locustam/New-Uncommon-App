using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ChecklistController : MonoBehaviour
{
    // List to store all toggle references
    public List<Toggle> toggles = new List<Toggle>();


    // Public function to clear all toggles (set them to 'unticked')
    public void ClearAllToggles()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false; // Untick the toggle
        }
    }
}
