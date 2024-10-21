using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StudentData
{

    public string _studentName = "N/A";
    public string _firstName;//jh _studentName should equal to _firstName + _firstName + lastName;  The first and last names are used as records.
    public string _lastName;

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
    public bool _isVeteran;//jh this is not in use currently
    public bool _isAlumni;
    public bool _isPatron;

    public bool _isStateSponsored;//jh this is not in use currently


    //jh Personal Info: This section is generated based on the bools and is dependent on them.
    //jh For example, if student is an alumni, the father and mother education should both show up as Uncommon Academy.
    public string _fatherName;//jh father name is random first name + child last name (we could do matrilineal surnames as well, this is subject to change)
    public string _fatherEducation;
    public string _motherName;
    public string _motherEducation;

    public string _nationality;

    public RaceData.NationType _nationType;


    //==========
    public int _legendaryStudentID;

    //======================

    //===========Student's Chance of Enrollment===============

    public bool willEnroll;

    //========================================================
}
