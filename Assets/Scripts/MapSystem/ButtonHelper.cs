using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour
{
    public Button targetButton;

    public void TriggerTargetButton()
    {
        if (targetButton != null)
        {
            targetButton.onClick.Invoke();
        }
    }
}