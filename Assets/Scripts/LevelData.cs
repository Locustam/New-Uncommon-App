using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = "Levels", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Multipliers")]
    public float academicMultiplier;
    public float financeMultiplier;
    [Header("How many student in total")]
    public int studentLeft;

    [Header("Student Required")]
    public int studentRequired = 7;

    public int initialScholarship = 500;

    //=====Testing=====
    [Range(0.0f, 100.0f)]
    public int goodStudentPercentage;
    [Range(0.0f, 100.0f)]
    public int badStudentPercentage;
    [Range(0.0f, 100.0f)]
    public int richStudentPercentage;
    [Range(0.0f, 100.0f)]
    public int poorStudentPercentage;

    //====Personal Info====
    //==scales==
    [Range(0.0f, 100.0f)]
    public int extroversionPercentage;
    [Range(0.0f, 100.0f)]
    public int magicalPersonalityPercentage;
    [Range(0.0f, 100.0f)]
    public int workplacePercentage;
    [Range(0.0f, 100.0f)]
    public int schedulePercentage;
    [Range(0.0f, 100.0f)]
    public int explorativityPercentage;
    [Range(0.0f, 100.0f)]
    public int psionicAffinityPercentage;


    [Range(0.0f, 100.0f)]
    public int verteranPercentage;
    [Range(0.0f, 100.0f)]
    public int alumniPercentage;
    [Range(0.0f, 100.0f)]
    public int patronPercentage;

    


    //==========


    //==bools===
    [Range(0.0f, 100.0f)]
    public int firstGenPercentage;
    [Range(0.0f, 100.0f)]
    public int stateSponsoredPercentage;

    public List<MiniGoalOptions> miniGoalPool = new();

    public List<Feature> features = new();

    public List<SingleMessage> singleMessages = new();
}

[System.Serializable]
public class MiniGoalOptions
{
    [Header("Copy one of the following exact string(with space included) to the goal : \nintroverted,extroverted,calm, emotional,night owl,early bird,careful,\nexplorativenon-psychic,psychic,1st-gen,legacy,patron,oyveka")]
    public string goal;
    public int requiredNum;
    
    
}

[System.Serializable]
public class Feature
{
    public enum feature { miniGoals, preferences, booleans, legendaryStudents,legendaryStudent1,legendaryStudent2,legendaryStudent3}
    public feature includedFeature;
}

