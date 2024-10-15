using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentAdmissionManager : MonoBehaviour
{

    //Student Admission Manager
    //Manages admitted, waitlisted, and rejected students
    //

    public static StudentAdmissionManager Instance { get; private set; } // Singleton instance

    [Header("Component References")]
    public StudentInfo studentInfo;
    [SerializeField] PreferencesManager preferencesManager;
    [SerializeField] BooleanManager booleanManager;
    public NewspaperManager newspaperManager;
    [SerializeField] ChecklistController checklistController;
    [SerializeField] ChecklistReviewManager checklistReveiwManager;

    public Animator gameAnimator;
    [SerializeField] Animator studentImageAnimator;
    [SerializeField] StudentGenerationManager studentGenerationManager;
    [SerializeField] TextFormater textFormater;

    public List<StudentData> admittedStudentList = new List<StudentData>();
    public List<StudentData> rejectedStudentList = new List<StudentData>();
    public List<StudentData> waitlistedStudentList = new List<StudentData>();


    

    [Header("Game Variable Texts")]
    [SerializeField] Color colorUrgent;
    [SerializeField] Color colorSafe;
    [SerializeField] TextMeshProUGUI studentLeftText;
    [SerializeField] TextMeshProUGUI studentAdmittedVSRequiredText;
    [SerializeField] TextMeshProUGUI averageFinanceText;
    [SerializeField] TextMeshProUGUI averageAcademicText;
    [SerializeField] TextMeshProUGUI totalScholarshipText;
    [SerializeField] Slider averageFinanceSlider;
    [SerializeField] Slider averageAcademicSlider;
    [SerializeField] Slider totalScholarshipSlider;

    [Header("Game Object References")]
    public GameObject studentContentInfoPrefab;
    public GameObject admittedStudentContentParent;
    public GameObject rejectedStudentContentParent;
    public GameObject waitlistedStudentContentParent;

    


    [Header("===========================")]



    [Header("How many student in total")]
    public int studentLeft;

    [Header("Student Required")]
    public int studentRequired = 7;

    [Header("Scholarship Bonus")]
    public int patronScholarshipBonus = 50;


    [Header("===========================")]
    [Header("   ")]
    public int studentAdmitted = 0;

    public int financeRequired = 100;

    public int financeMidValue = 50;
    public int academicMidValue = 50;
    public int averageFinance = 50;
    public int averageAcademic = 50;
    public int totalScholarship;
    public int initialScholarship = 1000;
    public int financeDangerLine = 40;
    public int academicDangerLine = 40;

    public float financeMultiplier = 0.1f;
    public float academicMultiplier = 0.2f;


    //====Personal Info=====

    public int firstGenStudent = 0;
    public int alumniStudent = 0;
    public int patronStudent = 0;

    //======================


    private void Awake()
    {

        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this; // Assign this instance as the singleton instance
        }
        else
        {
            Destroy(gameObject); // Destroy this instance because another one already exists
            return;
        }

        

    }

    private void Start()
    {
        RandomlyPresentAStudent();
        UpdateAllVisuals();
    }

    private void Update()
    {
        //==============Win/Lose check===============
        #region
        //jh this is a check of end game, it is a series of condtionals
        //Currently GameManager.Instance.WinOrNot is triggered inside animation event
        if (GameManager.Instance.inGame)
        {
            if (studentAdmitted > studentRequired)
            {
                GameManager.Instance.win = false;              
                GameManager.Instance.LoadOut();
                //GameManager.Instance.GameLose();
            }

            if (studentLeft <= 0)
            {
                if (studentAdmitted < studentRequired)
                {
                    GameManager.Instance.win = false;
                    GameManager.Instance.LoadOut();
                    //GameManager.Instance.GameLose();
                }
                else
                {
                    //GameManager.Instance.GameCalc();
                    GameManager.Instance.win = true;
                    SoundManager.Instance.PlaySFX("Clear");
                    GameManager.Instance.LoadOut();
                }

            }

            if (studentAdmitted == studentRequired)
            {
                if (averageFinance > financeDangerLine && averageAcademic > academicDangerLine)
                {
                    //GameManager.Instance.GameCalc();
                    GameManager.Instance.win = true;
                    GameManager.Instance.LoadOut();
                }
                else
                {
                    //GameManager.Instance.GameLose();
                    GameManager.Instance.win = false;
                    GameManager.Instance.LoadOut();
                }

            }

        }
        #endregion
        //=============================================
    }



    public void UpdateAllVisuals()
    {
        LegendaryStudentManager.Instance.ClearLegendaryStudentVisuals();
        checklistController.ClearAllToggles();
        if (averageFinance <= financeDangerLine)
        {
            averageFinanceText.color = colorUrgent;
            averageFinanceText.text = "<shake a = 2>" + averageFinance.ToString() + "</shake>";
        }
        else
        {
            averageFinanceText.color = colorSafe;
            averageFinanceText.text = averageFinance.ToString();
        }
        if (averageAcademic <= academicDangerLine)
        {
            averageAcademicText.color = colorUrgent;
            averageAcademicText.text = "<shake a = 2>" + averageAcademic.ToString() + "</shake>";
        }
        else
        {
            averageAcademicText.color = colorSafe;
            averageAcademicText.text = averageAcademic.ToString();
        }

        if(totalScholarship <= 200)
        {
            totalScholarshipText.color = colorUrgent;
            totalScholarshipText.text = "<shake a = 2>" + totalScholarship.ToString() + "</shake>";
        }
        else
        {
            totalScholarshipText.color=colorSafe;
            totalScholarshipText.text = totalScholarship.ToString();
        }
            

        averageFinanceSlider.value = averageFinance;
        averageAcademicSlider.value = averageAcademic;
        totalScholarshipSlider.value = totalScholarship;
        if(studentLeft + studentAdmitted <= studentRequired)
        {
            studentAdmittedVSRequiredText.color = colorUrgent;
            studentAdmittedVSRequiredText.text = "<shake a = 2>" + studentAdmitted.ToString() + "/" + studentRequired + "</shake>";

        }
        else
        {
            studentAdmittedVSRequiredText.color = colorSafe;
            studentAdmittedVSRequiredText.text = studentAdmitted.ToString() + "/" + studentRequired;
        }
        
        if (studentLeft <= 10)
        {
            studentLeftText.color = colorUrgent;
            studentLeftText.text = "<shake a = 2>" + studentLeft.ToString() + "</shake>";
        }
        else
        {
            studentLeftText.color = colorSafe;
            studentLeftText.text = studentLeft.ToString();
        }
    }
    public void UpdateRejectVisuals()
    {
       /* averageFinanceText.text = averageFinance.ToString();
        averageAcademicText.text = averageAcademic.ToString();
        totalScholarshipText.text = totalScholarship.ToString();
        averageFinanceSlider.value = averageFinance;
        averageAcademicSlider.value = averageAcademic;
        totalScholarshipSlider.value = totalScholarship;
        studentAdmittedVSRequiredText.text = studentAdmitted.ToString() + "/" + studentRequired;*/
        if (studentLeft < 10)
        {
            studentLeftText.text = "<shake a = 2>" + studentLeft.ToString() + "</shake>";
        }
        else studentLeftText.text = studentLeft.ToString();
    }


    public void AdmitCurrentStudent()
    {
        StudentData data = studentInfo.data;
        if (data == null)
        {
            data = studentInfo.data;
        }
        if(GameManager.Instance.inGame)
        {
            if (CanAdmit(data)) 
            {
                bool checkListPassed;
                if(checklistReveiwManager.ReviewChecklist(data))
                {
                    checkListPassed = true;
                }
                else
                {
                    checkListPassed = false;
                }

                SoundManager.Instance.PlaySFX("Admit");
                SoundManager.Instance.PlaySFX("Click_OK");
                SoundManager.Instance.PlaySFX("FileInCrush", 0.5f);
                if (!admittedStudentList.Contains(data))
                {
                    admittedStudentList.Add(data);
                }
                if (data != null)
                {
                    
                    gameAnimator.SetTrigger("Accept");
                    studentAdmitted += 1;
                    studentLeft -= 1;

                    var scholarshipCost = (financeRequired - data._finance);
                    if (LegendaryStudentManager.Instance.unmeiiaNystalFreeEffect)
                    {
                        if(data._nationality == "Tendiyu" || data._nationality == "Gessurd")
                        {
                            scholarshipCost = 0;
                        }
                        Mathf.RoundToInt(scholarshipCost * 1.3f);

                    }
                    totalScholarship -= scholarshipCost;
                    if (totalScholarship < 0)
                    {
                        totalScholarship = 0;
                    }

                    if (data._isPatron)
                    {
                        if (LegendaryStudentManager.Instance.patronAddPoolEffect)
                        {
                            Debug.Log("patron added student");
                            studentLeft += 5;
                        }
                        else
                        {
                            totalScholarship += patronScholarshipBonus;
                        }
                    }

                    averageFinance += Mathf.RoundToInt((data._finance - financeMidValue) * financeMultiplier);
                    averageAcademic += Mathf.RoundToInt((data._academic - academicMidValue) * academicMultiplier);
                    
                    firstGenStudent += data._isFirstGen ? 1 : 0;
                    alumniStudent += data._isAlumni ? 1 : 0;
                    patronStudent += data._isPatron ? 1 : 0;

                    //===Legendary====
                    if (studentInfo.isLegendary && LegendaryStudentManager.Instance.lastStudentData != null && data == LegendaryStudentManager.Instance.lastStudentData)
                    {
                        LegendaryStudentManager.Instance.UnlockCurrentScanedLegendaryStudent();
                        StudentGenerationManager.Instance.remainingLegendaryStudentList.Remove(data);
                    }
                    //================

                    //===Mini Goal Datas===
                    #region
                    if (checklistPassed) {
                        for (int i = 0; i < MiniGoalManager.Instance.miniGoalDatas.Count; i++)
                        {
                            MiniGoalData goal = MiniGoalManager.Instance.miniGoalDatas[i];

                            switch (goal.label)
                            {
                                case "introverted":
                                    if (data._extroversion <= 2)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "extroverted":
                                    if (data._extroversion >= 4)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "calm":
                                    if (data._magicalPersonality <= 2)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "emotional":
                                    if (data._magicalPersonality >= 4)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "night owl":
                                    if (data._schedule <= 2)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "early bird":
                                    if (data._schedule >= 4)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "careful":
                                    if (data._explorativity <= 2)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "explorative":
                                    if (data._explorativity >= 4)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "non-psychic":
                                    if (data._psionicAffinity <= 2)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "psychic":
                                    if (data._psionicAffinity >= 4)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "1st-gen":
                                    if (data._isFirstGen)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "legacy":
                                    if (data._isAlumni)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "patron":
                                    if (data._isPatron)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                                case "oyveka":
                                    if (data._nationType == RaceData.NationType.Ovyeka)
                                    {
                                        MiniGoalManager.Instance.miniGoalDatas[i].UpdateProgress(1);
                                    }

                                    break;

                            }
                        }
                        
                    }
                    #endregion
                    MiniGoalManager.Instance.UpdateMiniGoalVisuals();


                    //======================


                    Invoke("UpdateAllVisuals", 0.3f);

                    
                }
                Invoke("RandomlyPresentAStudent", 0.3f);
            }
        }
        
    }

    public void DecreasePlayerHealth(int amount)
    {

    }
    public bool CanAdmit(StudentData data)
    {
        if (totalScholarship >= financeRequired - data._finance)
        {
            
            return true;
        }
        else
        {
            if(LegendaryStudentManager.Instance.unmeiiaNystalFreeEffect){
                if(data._nationality == "Tendiyu" || data._nationality == "Gessurd")
                {
                    return true;
                }
            }
            AnnouncementManager.Instance.DisplayAnnouncement("Not enough scholarship");
            return false;
        }
    }

    public void RejectCurrentStudent()
    {
        if(GameManager.Instance.inGame)
        {
            SoundManager.Instance.PlaySFX("Reject");
            SoundManager.Instance.PlaySFX("Click_OK");
            SoundManager.Instance.PlaySFX("CrumbleCrush");
            StudentData data = studentInfo.data;
            if (data == null)
            {
                data = studentInfo.data;
            }

            if (!rejectedStudentList.Contains(data))
            {
                
                rejectedStudentList.Add(data);
            }
            gameAnimator.SetTrigger("Reject");
            studentLeft -= 1;
            Invoke("RandomlyPresentAStudent", 0.3f);
            Invoke("UpdateAllVisuals", 0.3f);

            //==========legendary=============
            LegendaryStudentManager.Instance.ClearCurrentScannedLegendaryStudent();



            //================================
        }
        
    }

    /*
    public void WaitlistCurrentStudent()
    {
        StudentData data = studentInfo.data;

        if (!waitlistedStudentList.Contains(data))
        {
            waitlistedStudentList.Add(data);
        }
        Invoke("RandomlyPresentAStudent",0.3f);
    }
    */


    public void RandomlyPresentAStudent()
    {

        //=====Generate a new student=====
        //In essense, student are genearted the moment the previous student is admited or rejected, instead of generated in advance.
        //Student Generation Manager has this giant public function that will return a StudentData, which contains all the info for that particular student.


        StudentData data = studentGenerationManager.RandomGenerateStudent();

        //================================


        //====Visual Updates====

        //animation
        studentImageAnimator.SetTrigger("LoadIn");

        studentInfo.UpdateStudentInfo(data);//Student info is a visual controller, so does preferences manager and boolean manager.
        preferencesManager.SetBars(data._extroversion-1,data._magicalPersonality-1,data._schedule-1,data._explorativity-1,data._psionicAffinity-1);
        booleanManager.SetChecks(data);
        textFormater.Determine();
        //====Visual Updates====

    }

    /*
    public void UpdateAdmittedStudentInfo()
    {
        // Destroy all child objects of admittedStudentContentParent
        foreach (Transform child in admittedStudentContentParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate studentContentInfoPrefab for each StudentData in admittedStudentList
        foreach (StudentData studentData in admittedStudentList)
        {
            // Instantiate the prefab as a child of admittedStudentContentParent
            GameObject studentContent = Instantiate(studentContentInfoPrefab, admittedStudentContentParent.transform);

            studentContent.GetComponent<StudentInfoContent>().data = studentData;
        }
    }

    
    public void UpdateRejectedStudentInfo()
    {
        // Destroy all child objects of admittedStudentContentParent
        foreach (Transform child in rejectedStudentContentParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate studentContentInfoPrefab for each StudentData in admittedStudentList
        foreach (StudentData studentData in rejectedStudentList)
        {
            // Instantiate the prefab as a child of admittedStudentContentParent
            GameObject studentContent = Instantiate(studentContentInfoPrefab, rejectedStudentContentParent.transform);

            studentContent.GetComponent<StudentInfoContent>().data = studentData;
        }
    }
    

    
    public void UpdateWaitlistedStudentInfo()
    {
        // Destroy all child objects of admittedStudentContentParent
        foreach (Transform child in waitlistedStudentContentParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate studentContentInfoPrefab for each StudentData in admittedStudentList
        foreach (StudentData studentData in waitlistedStudentList)
        {
            // Instantiate the prefab as a child of admittedStudentContentParent
            GameObject studentContent = Instantiate(studentContentInfoPrefab, waitlistedStudentContentParent.transform);

            studentContent.GetComponent<StudentInfoContent>().data = studentData;
        }
    }
    */

}
