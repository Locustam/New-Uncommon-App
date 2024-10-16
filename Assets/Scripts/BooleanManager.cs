using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BooleanManager : MonoBehaviour
{
    [SerializeField] Toggle alumniToggle;
    [SerializeField] Toggle firstGenToggle;
    [SerializeField] Toggle patronToggle;
    [SerializeField] TextMeshProUGUI fatherNameText;
    [SerializeField] TextMeshProUGUI fatherEducationText;
    [SerializeField] TextMeshProUGUI motherNameText;
    [SerializeField] TextMeshProUGUI motherEducationText;



    public void SetChecks(StudentData data)
    {
        
        //if(data._isAlumni)
        //{
        //    alumniToggle.SetActive(true);
        //}
        //else
        //{
        //    alumniToggle.SetActive(false);
        //}

        //if (data._isFirstGen)
        //{
        //    firstGenToggle.SetActive(true);
        //}
        //else
        //{
        //    firstGenToggle.SetActive(false);
        //}

        if (data._isPatron)
        {
            patronToggle.isOn = true;
        }
        else
        {
            patronToggle.isOn = false;
        }

        fatherNameText.text = "Father: " + data._fatherName;
        fatherEducationText.text = "Education: " + data._fatherEducation;
        motherNameText.text = "Mother: " + data._motherName;
        motherEducationText.text = "Education: " + data._motherEducation;
        Debug.Log("father name" + fatherNameText.text);
    }

}
