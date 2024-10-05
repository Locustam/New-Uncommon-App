using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class LegendaryStudentVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool studentLocked = true;
    public List<GameObject> unlockedVisuals = new();
    public List<GameObject> lockedVisuals = new();
    public List<GameObject> effectVisuals = new();
    public bool studentSelected = false;
    public int legendaryStudentID;
    public GameObject confirmSelectPanel;
    public GameObject confirmCancelPanel;
    public LegendaryConfirmUI confirmUI;
    bool isHovering = false;
    public int hireCost = 50;
    

    // Start is called before the first frame update
    void Awake()
    {
        SetLockState(studentLocked);
        
    }

    [ContextMenu("Toggle Lock State")]
    private void ToggleLockState()
    {
        SetLockState(!studentLocked);
    }

    // Sets the lock state of visuals based on the given state
    public void SetLockState(bool lockState)
    {
        studentLocked = lockState;
        GetComponent<Toggle>().interactable = lockState;
        Debug.Log("setting" + legendaryStudentID + "state as: " + lockState);
        // Set the active state of each GameObject in lockedVisuals and unlockedVisuals
        foreach (GameObject lockedVisual in lockedVisuals)
        {
            lockedVisual.SetActive(lockState);
        }
        foreach (GameObject unlockedVisual in unlockedVisuals)
        {
            unlockedVisual.SetActive(!lockState);
            Debug.Log("unlockedVisuals: " + unlockedVisuals);
        }
        if(!lockState)
        {
             GetComponent<Toggle>().interactable = true;
        }
        else{
            GetComponent<Toggle>().interactable = false;
        }
    }

    public void ActivateEffectsVisuals()
    {
        foreach (GameObject unlockedVisual in effectVisuals)
        {
            unlockedVisual.SetActive(true);
        }
    }

        public void DectivateEffectsVisuals()
    {
        foreach (GameObject unlockedVisual in effectVisuals)
        {
            unlockedVisual.SetActive(false);
        }
    }
    public void ToggleLegendaryStudentActivate()
    {
        SoundManager.Instance.PlaySFX("ChatOpen");
        if (!confirmUI.isPanelOpen)
        {
            Toggle toggle = GetComponent<Toggle>();
            if (!studentLocked)
            {
                if (toggle.isOn)
                {
                    if (isHovering == true)
                    {
                        SoundManager.Instance.PlaySFX("ChatOpen");
                        confirmUI.OpenConfirmSelectPanel(this.GetComponent<LegendaryStudentVisuals>());
                    }
                    studentSelected = true;
                }
                else
                {
                    if (isHovering == true)
                    {
                        SoundManager.Instance.PlaySFX("ChatClose");
                        confirmUI.OpenConfirmDeselectPanel(this.GetComponent<LegendaryStudentVisuals>());
                    }
                    studentSelected = false;
                }
            }
        }
        
    }

        // This function is called when the mouse enters the UI element's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    // This function is called when the mouse exits the UI element's area
    public void OnPointerExit(PointerEventData eventData)
    {
         isHovering = false;
    }

    public bool CalculateConfirmCondition()
    {
        if(StudentAdmissionManager.Instance.totalScholarship >= hireCost)
        {
            Debug.Log("calculated confirm condition as true");
            StudentAdmissionManager.Instance.totalScholarship -= hireCost;
            StudentAdmissionManager.Instance.UpdateAllVisuals();
            ActivateEffectsVisuals();
            if(legendaryStudentID == 0)
            {
                
                LegendaryStudentManager.Instance.moreAcademicLessMoneyEffect = true;
                LegendaryStudentManager.Instance.legendaryEffectIcon[0].SetActive(true);
                LegendaryStudentManager.Instance.legendaryEffectIcon[1].SetActive(false);
                LegendaryStudentManager.Instance.legendaryEffectIcon[2].SetActive(false);
                LegendaryStudentManager.Instance.legendaryStudentVisualsList[1].DectivateEffectsVisuals();
                LegendaryStudentManager.Instance.legendaryStudentVisualsList[2].DectivateEffectsVisuals();
            }
            if(legendaryStudentID == 1)
            {
                LegendaryStudentManager.Instance.patronAddPoolEffect = true;
                
                LegendaryStudentManager.Instance.legendaryEffectIcon[1].SetActive(true);
                LegendaryStudentManager.Instance.legendaryEffectIcon[0].SetActive(false);
                LegendaryStudentManager.Instance.legendaryEffectIcon[2].SetActive(false);
                LegendaryStudentManager.Instance.legendaryStudentVisualsList[0].DectivateEffectsVisuals();
                LegendaryStudentManager.Instance.legendaryStudentVisualsList[2].DectivateEffectsVisuals();
            }
            if(legendaryStudentID == 2)
            {
                LegendaryStudentManager.Instance.unmeiiaNystalFreeEffect= true;
                LegendaryStudentManager.Instance.legendaryEffectIcon[2].SetActive(true);
                LegendaryStudentManager.Instance.legendaryEffectIcon[0].SetActive(false);
                LegendaryStudentManager.Instance.legendaryEffectIcon[1].SetActive(false);
                LegendaryStudentManager.Instance.legendaryStudentVisualsList[1].DectivateEffectsVisuals();
                LegendaryStudentManager.Instance.legendaryStudentVisualsList[0].DectivateEffectsVisuals();
            }

            return true;
        }

        return false;
    }

    public void DisableStudentEffect()
    {
        if(legendaryStudentID == 0)
        {
            LegendaryStudentManager.Instance.moreAcademicLessMoneyEffect = false;
            LegendaryStudentManager.Instance.legendaryEffectIcon[0].SetActive(false);

        }

        if(legendaryStudentID == 1)
        {
            
            LegendaryStudentManager.Instance.patronAddPoolEffect = false;
            LegendaryStudentManager.Instance.legendaryEffectIcon[1].SetActive(false);
        }
        if(legendaryStudentID == 2)
        {
            LegendaryStudentManager.Instance.unmeiiaNystalFreeEffect= false;
            LegendaryStudentManager.Instance.legendaryEffectIcon[2].SetActive(false);
        }

        DectivateEffectsVisuals();
    }

    
}
