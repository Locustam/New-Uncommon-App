using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public List<StateDescription> stateContentList = new List<StateDescription>();
    [SerializeField] TextMeshProUGUI titleObject, descriptionObject;
    [SerializeField] Image emblem;
    public int currentStateNum;
    StateDescription currentState;

    public void Start()
    {
        currentStateNum = 5;
        DisplayState();
    }
    public void DisplayState()
    {
        currentState = stateContentList[currentStateNum];
        if(currentState != null )
        {
            titleObject.text = currentState.stateName;
            descriptionObject.text = currentState.description;
            emblem.sprite = currentState.emblem;
        }      
    }

    public void SetUmeiia()
    {
        currentStateNum = 0;
    }
    public void SetNystal()
    {
        currentStateNum = 1;
    }
    public void SetTendiyu()
    {
        currentStateNum = 2;
    }
    public void SetOyveka()
    {
        currentStateNum = 3;
    }
    public void SetGessurd() 
    {
        currentStateNum = 4;
    }

}

[System.Serializable]
public class StateDescription
{ 
    public string stateName;
    public string description;
    public Sprite emblem;
}

