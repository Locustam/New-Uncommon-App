using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Message : MonoBehaviour
{

    //========Data============

    //========================

    //========Controls========
    public string currentText;
    public Sprite currentSprite;


    //========================


    //========Visuals=========
    [SerializeField]TextMeshProUGUI textContentTxt;
    [SerializeField] Image imageContentImg;
    //========================

    void Start()
    {
        UpdateVisuals();

    }

    public void UpdateVisuals()
    {
        textContentTxt.text = currentText;
        imageContentImg.sprite = currentSprite;

    }



}
