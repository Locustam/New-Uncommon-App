using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StudentInfo : MonoBehaviour
{
    public StudentData data;

    //===StudentDataVisuals===
    public TextMeshProUGUI studentNameText;
    public TextMeshProUGUI studentDescriptionText;
    public Slider financeSlider;
    public Slider academicSlider;
    public bool isLegendary;

    [SerializeField] Image AImage;
    [SerializeField] Image BImage;
    [SerializeField] Image CImage;
    [SerializeField] Image DImage;
    [SerializeField] Image EImage;
    [SerializeField] Image FImage;
    [SerializeField] Image GImage;
    [SerializeField] Image HImage;
    //public TextMeshProUGUI 
    [SerializeField] Image IImage;

    //========================


    public void UpdateStudentInfo(StudentData _data)
    {
        data = _data;

        AImage.sprite = data._ASprite;

        if (data._BSprite != null)
        {
            BImage.enabled = true;
            BImage.sprite = data._BSprite;
        }else
        {
            BImage.enabled = false;
        }



        CImage.sprite = data._CSprite;
        DImage.sprite = data._DSprite;
        EImage.sprite = data._ESprite;
        FImage.sprite = data._FSprite;
        

        if (data._GSprite != null)
        {
            GImage.enabled = true;
            GImage.sprite = data._GSprite;
        }
        else
        {
            GImage.enabled = false;
        }

        if(data._HSprite != null)
        {
            HImage.enabled = true;
            HImage.sprite = data._HSprite;

        }else
        {
            HImage.enabled = false;
        }

        if (data._ISprite != null)
        {
            IImage.enabled = true;
            IImage.sprite = data._ISprite;
        }

        studentNameText.text = data._studentName;
        financeSlider.value = data._finance;
        academicSlider.value = data._academic;

        //setting dangerline for personal finance
        SliderVisuals financeSliderVisuals = financeSlider.GetComponent<SliderVisuals>();
        if(financeSliderVisuals != null)
        {
            //Danger line is the average finance needed per student
            financeSliderVisuals.threshHolds[1] = StudentAdmissionManager.Instance.financeDangerLine / StudentAdmissionManager.Instance.studentRequired;
        }

    }

    public void ScanStudent()
    {
        SoundManager.Instance.PlaySFX("Scan Beep3");
    }

    public void Reveal()
    {
        if(isLegendary)
        {
            SoundManager.Instance.PlaySFX("Reveal2");
        }
    }

}
