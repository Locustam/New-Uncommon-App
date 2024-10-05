using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegendaryConfirmUI : MonoBehaviour
{
    public LegendaryStudentVisuals previousLegendary;
    public LegendaryStudentVisuals currentLegendary;
    [SerializeField] GameObject ConfirmSelectPanelObject;
    [SerializeField] GameObject ConfirmDeselectPanelObject;
    public bool isPanelOpen = false;

    public void OpenConfirmSelectPanel(LegendaryStudentVisuals vis)
    {
        SoundManager.Instance.PlaySFX("Click_ChatOpen");
        if (currentLegendary != null)
        {
            previousLegendary = currentLegendary;
        }
        currentLegendary = vis;
        ConfirmSelectPanelObject.SetActive(true);
        isPanelOpen = true;
    }

    public void CloseConfirmSelectPanel(bool remainToggleIsOn)
    {
        SoundManager.Instance.PlaySFX("Click_ChatClose");
        if (remainToggleIsOn) {
            currentLegendary.GetComponent<Toggle>().isOn = true;
        }else{
            currentLegendary.GetComponent<Toggle>().isOn = false;
        }
        
        ConfirmSelectPanelObject.SetActive(false);
        isPanelOpen = false;
    }

    public void OpenConfirmDeselectPanel(LegendaryStudentVisuals vis)
    {
        SoundManager.Instance.PlaySFX("Click_ChatOpen");
        if (currentLegendary != null)
        {
            previousLegendary = currentLegendary;
        }
        currentLegendary = vis;
        ConfirmDeselectPanelObject.SetActive(true);
        isPanelOpen = true;
    }

    public void CloseConfirmDeselectPanel(bool remainToggleIsOn)
    {
        SoundManager.Instance.PlaySFX("Click_ChatClose");
        if (remainToggleIsOn) {
            currentLegendary.GetComponent<Toggle>().isOn = true;
        }else{
            currentLegendary.GetComponent<Toggle>().isOn = false;
        }
        ConfirmDeselectPanelObject.SetActive(false);
        isPanelOpen = false;
    }

    public void SetCurrentStudentSelected(bool isStudentSelected)
    {
        if (currentLegendary != null )
        {
            currentLegendary.studentSelected = isStudentSelected;
            currentLegendary.GetComponent<Toggle>().isOn = isStudentSelected;
        }
    }

     public void SetPreviousStudentSelected(bool isStudentSelected)
    {
        if (previousLegendary != null)
        {
            previousLegendary.studentSelected = isStudentSelected;
            previousLegendary.GetComponent<Toggle>().isOn = isStudentSelected;
        }else{
            SetCurrentAndPreviousToNull();
        }
    }

    public void SetCurrentStudentToPrevious()
    {
        if (previousLegendary != null)
        {
            currentLegendary = previousLegendary;
        }
    }

    public void SetCurrentAndPreviousToNull()
    {
        currentLegendary = null;
        previousLegendary = null;
    }

    public void UltimateConfirmSelectButton()
    {
        
        
        if (currentLegendary.CalculateConfirmCondition())
        {
            SoundManager.Instance.PlaySFX("Click_OK");
            SoundManager.Instance.PlaySFX("FinalHire");
            CloseConfirmSelectPanel(true);
            SetCurrentStudentSelected(true);
        }
        else
        {
            SoundManager.Instance.PlaySFX("Click_Confirm");
        }
    }

    public void UltimateCancelSelectButton()
    {
        SoundManager.Instance.PlaySFX("Click_OK");
        CloseConfirmSelectPanel(false);
        SetCurrentStudentSelected(false);
        SetPreviousStudentSelected(true);
        SetCurrentStudentToPrevious();
    }

    public void UltimateConfirmDeselectButton()
    {
        SoundManager.Instance.PlaySFX("Click_OK");
        SoundManager.Instance.PlaySFX("FinalCancel");
        currentLegendary.DisableStudentEffect();
        CloseConfirmDeselectPanel(false);
        SetCurrentStudentSelected(false);
        SetCurrentAndPreviousToNull();
        
    }

    public void UltimateCancelDeselectButton()
    {
        SoundManager.Instance.PlaySFX("Click_OK");
        CloseConfirmDeselectPanel(true);
        SetCurrentStudentSelected(true);
        SetCurrentAndPreviousToNull();
    }
}
