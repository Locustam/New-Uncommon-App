using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistReviewManager : MonoBehaviour
{
    [SerializeField] Toggle alumniToggle;
    [SerializeField] Toggle firstGenToggle;
    // Start is called before the first frame update
    public bool ReviewChecklist(StudentData data)
    {
        if (data._isAlumni == alumniToggle.isOn &&
            data._isFirstGen == firstGenToggle.isOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
