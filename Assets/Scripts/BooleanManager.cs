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

        fatherNameText.text = data._fatherName;
        fatherEducationText.text = data._fatherEducation;
        motherNameText.text = data._motherName;
        motherEducationText.text = data._motherEducation;
        Debug.Log("father name" + fatherNameText.text);
    }

}
