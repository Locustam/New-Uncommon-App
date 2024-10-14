using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StudentData
{

    public string _studentName = "N/A";
    public string _studentDescription;

    public string _studentRace = "N/A";

    //====General Info=====
    public int _academic;
    public int _finance;

    //=====================




    //=====Sprites=========
    public Sprite _ASprite;
    public Sprite _BSprite;
    public Sprite _CSprite;
    public Sprite _DSprite;
    public Sprite _ESprite;
    public Sprite _FSprite;
    public Sprite _GSprite;

    public Sprite _HSprite;
    public Sprite _ISprite;

    //=====================



    //====Personal Info====
    //==preferences==
    public int _extroversion;
    public int _magicalPersonality;
    public int _workplace;
    public int _schedule;
    public int _explorativity;
    public int _psionicAffinity;


    //==========


    //==bools===

    public bool _isFirstGen;
    public bool _isVeteran;
    public bool _isAlumni;
    public bool _isPatron;

    public bool _isStateSponsored;


    public string _nationality;

    public RaceData.NationType _nationType;


    //==========

    public int _legendaryStudentID;

    //======================


}
