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
    public Slider needScholarshipSlider;
    public MeritScholarshipController meritScholarshipController;
    public bool isLegendary;

    [SerializeField] TextMeshProUGUI studentFinanceText;
    [SerializeField] TextMeshProUGUI studentAcademicText;


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
        financeSlider.maxValue = StudentAdmissionManager.Instance.financeRequired;
        financeSlider.value = data._finance;
        
        academicSlider.value = data._academic;

        //setting dangerline for personal finance
        SliderVisuals financeSliderVisuals = financeSlider.GetComponent<SliderVisuals>();
        if(financeSliderVisuals != null)
        {
            //Danger line is the average finance needed per student
            financeSliderVisuals.threshHolds[1] = StudentAdmissionManager.Instance.financeRequired-1;
        }

        if(data._isNeedScholarship)
        {
            needScholarshipSlider.enabled = true;
            Color color = needScholarshipSlider.fillRect.GetComponent<Image>().color;
            color.a = 1;
            needScholarshipSlider.fillRect.GetComponent<Image>().color = color;
        }
        else
        {
            needScholarshipSlider.enabled = false;
            Color color = needScholarshipSlider.fillRect.GetComponent<Image>().color;
            color.a = 0;
            needScholarshipSlider.fillRect.GetComponent<Image>().color = color;
            
        }

        studentAcademicText.text = data._academic.ToString();
        //studentFinanceText.text = data._finance.ToString();
        studentFinanceText.text = data._financeWithScholarship.ToString() + "(" + data._finance.ToString()+ " + " + ( data._financeWithScholarship - data._finance).ToString() + ")";


        meritScholarshipController.minValue = data._finance;
        meritScholarshipController.maxValue = StudentAdmissionManager.Instance.financeRequired;
        meritScholarshipController.InitializeSlider();  


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
