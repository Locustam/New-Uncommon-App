using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using TMPro;

public class LegendaryStudentManager : MonoBehaviour
{
    public static LegendaryStudentManager Instance;
    [SerializeField] StudentInfo studentInfo;
    [SerializeField] Animator studentInfoAnimator;
    StudentData lastStudentData; 
    [SerializeField] List<Image> bodyParts;
    public List<LegendaryStudentVisuals> legendaryStudentVisualsList = new();
    public bool moreAcademicLessMoneyEffect;
    public bool unmeiiaNystalFreeEffect;
    public bool patronAddPoolEffect;
    public int currentScannedLegendaryStudentID = -1;
    public List<bool> legendaryStudentUnlockStates = new();
    public List<GameObject> legendaryEffectIcon = new();
    public int ScanLeft;
    public int InitialScanNumber;
    public TextMeshProUGUI ScanLeftText;

    private void Awake()
    {
        // Singleton pattern enforcement
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);  // Optionally make this object persistent
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeUnlockStates();
        LoadUnlockStates();
    }

    void Start()
    {
        ScanLeft = InitialScanNumber;
        ScanLeftText.text = ScanLeft.ToString();
    }

    private void InitializeUnlockStates()
    {
        // Initialize unlock states if not loaded
        if (legendaryStudentUnlockStates.Count == 0)
        {
            legendaryStudentUnlockStates = new List<bool>(new bool[legendaryStudentVisualsList.Count]);
        }
    }

    private void OnApplicationQuit()
    {
        SaveUnlockStates();
    }

    public void SaveUnlockStates()
    {
        Debug.Log("Saving legendarystudent data");
        string data = string.Join(",", legendaryStudentUnlockStates.Select(b => b ? "1" : "0"));
        PlayerPrefs.SetString("UnlockStates", data);
        PlayerPrefs.Save();
    }

    public void LoadUnlockStates()
    {
        string savedData = PlayerPrefs.GetString("UnlockStates", "");
        if (!string.IsNullOrEmpty(savedData))
        {
            Debug.Log("loading unlock states");
            List<bool> savedStates = savedData.Split(',').Select(s => s == "1").ToList();
            for (int i = 0; i < savedStates.Count; i++)
            {
                Debug.Log("setting " + i + "to " + savedStates[i]);
                legendaryStudentVisualsList[i].SetLockState(!savedStates[i]);
                legendaryStudentUnlockStates[i] = savedStates[i];
            }
        }
    }

    public void ClearSavedData()
    {
        PlayerPrefs.DeleteKey("UnlockStates");
        PlayerPrefs.Save();
        // Optionally reset states after clearing
        legendaryStudentUnlockStates = new List<bool>(new bool[legendaryStudentVisualsList.Count]);
        foreach (var visuals in legendaryStudentVisualsList)
        {
            visuals.SetLockState(false);
        }
    }
    
    public void ScanStudent()
    {
		if(ScanLeft >0){
        StudentData currentStudent = StudentAdmissionManager.Instance.studentInfo.data;
        if (currentStudent != lastStudentData)
        {
            ScanLeft -= 1;
            ScanLeftText.text = ScanLeft.ToString();
            SoundManager.Instance.PlaySFX("Click_OK");
            lastStudentData = currentStudent;
            studentInfoAnimator.SetTrigger("Scan");
            if (currentStudent._legendaryStudentID != 0)
            {
                SoundManager.Instance.PlaySFX("PreReveal");
                studentInfo.isLegendary = true;
                bodyParts[0].sprite = lastStudentData._ASprite;
                bodyParts[1].sprite = lastStudentData._BSprite;
                bodyParts[2].sprite = lastStudentData._CSprite;
                bodyParts[3].sprite = lastStudentData._DSprite;
                bodyParts[4].sprite = lastStudentData._ESprite;
                bodyParts[5].sprite = lastStudentData._FSprite;
                if (lastStudentData._GSprite != null)
                {
                    bodyParts[6].enabled = true;
                    bodyParts[6].sprite = lastStudentData._GSprite;
                }
                else
                {
                    bodyParts[6].enabled = false;
                }

                bodyParts[7].sprite = lastStudentData._HSprite;

                float initialValue = 1f; // Starting value
                float finalValue = -0.1f; // Ending value, consider changing to a positive value if this is outside expected range
                float duration = 2f; // Duration in seconds

                foreach (Image bodyPart in bodyParts)
                {
                    bodyPart.material = new Material(bodyPart.material);  // Create a new material instance for each body part

                    // Create the tween and assign a tag
                    DOVirtual.Float(initialValue, finalValue, duration, value =>
                    {
                        bodyPart.material.SetFloat("_FadeAmount", value);  // Apply the interpolated value to the shader property
                    })
                    .SetEase(Ease.InOutQuad)  // Optional: Set the easing function
                    .SetId(bodyPart.GetInstanceID());  // Set a unique ID based on the instance of the bodyPart
                }

                currentScannedLegendaryStudentID = currentStudent._legendaryStudentID - 1;
            }
            else
            {
                studentInfo.isLegendary = false;
            }
        }
        else
        {
            SoundManager.Instance.PlaySFX("Click_Confirm");
        }
	}
    }

    public void UnlockCurrentScanedLegendaryStudent()
    {
        if (currentScannedLegendaryStudentID >= 0)
        {            
            var id = legendaryStudentVisualsList[currentScannedLegendaryStudentID];
            id.SetLockState(false);
            legendaryStudentUnlockStates[currentScannedLegendaryStudentID] = true;
            SaveUnlockStates();
        }
    }

    public void ClearCurrentScannedLegendaryStudent()
    {
        currentScannedLegendaryStudentID = -1;
    }



   public void ClearLegendaryStudentVisuals()
   {
        KillTweens();
        foreach (Image bodyPart in bodyParts)
        {
            if (bodyPart != null)
            {
                bodyPart.material = new Material(bodyPart.material); // Create a new material instance for each body part
                bodyPart.material.SetFloat("_FadeAmount", 1); // Apply the interpolated value to the shader property
            }
        }

   }

    // Method to stop all tweens
    public void KillTweens()
    {
        foreach (Image bodyPart in bodyParts)
        {
            DOTween.Kill(bodyPart.GetInstanceID());  // Kills all tweens with this specific ID
        }
    }




   
}
