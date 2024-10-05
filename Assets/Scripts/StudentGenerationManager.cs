using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StudentGenerationManager : MonoBehaviour
{
    public static StudentGenerationManager Instance;
    //=====Testing=====
    [Header("Basic Stats")]
    [Range(0.0f, 100.0f)]
    public int goodStudentPercentage;
    [Range(0.0f, 100.0f)]
    public int badStudentPercentage;
    [Range(0.0f, 100.0f)]
    public int richStudentPercentage;
    [Range(0.0f, 100.0f)]
    public int poorStudentPercentage;
    [Space]
    //====Personal Info====
    //==scales==
    [Header("Personal Info")]
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
    [Space]
    [Range(0.0f, 100.0f)]
    public int verteranPercentage;
    [Range(0.0f, 100.0f)]
    public int alumniPercentage;
    [Range(0.0f, 100.0f)]
    public int patronPercentage;
    [Space]

    [Header("Nation Info")]
    [Range(0.0f, 100.0f)]
    public int firstNationPercentage;

    [Range(0f,100.0f)]
    [SerializeField] float lengedaryStudentPercentage;



    //==========


    //==bools===
    // [SerializeField] bool isVeteranPercentage;
    // [SerializeField] bool isAlumniPercentage;
    // public bool isPatronPercentage;
    [Range(0.0f, 100.0f)]
    public int firstGenPercentage;
    [Range(0.0f, 100.0f)]
    public int stateSponsoredPercentage;
    //==========

    //========Race===========  
    public List<string> raceList = new List<string>();
    private List<RaceData> raceDatas = new List<RaceData>();




    //========================

    //======Legendary Student=======
    public List<StudentData> allLegendaryStudentList;
    public List<StudentData> remainingLegendaryStudentList;



    //==============================

    //=================

    //======Visuals Inits========
    public List<string> firstNames;
    [SerializeField] List<string> lastNames;
    [SerializeField] TextFormater textFormater;
    //================================


    // Start is called before the first frame update
    void Awake()
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

        var _firstNames = new[]{"Kilay", "Huja", "UIyenw", "Toni", "Lia", "Charlotte", "Kandy", "Tynamous", "Wilhelm",
            "Priah", "Ume", "Queha", "Boe", "Yannis", "Eiha", "Kitami", "Xenon", "Pasha", "Zules", "Rodger", "Fabian", "Tuni", "Wuhue", "Lyuen", "Starlight",
            "Virilous", "Isla", "Yenwiu", "Nowi", "Puipui", "Navi", "Geb", "UIenn", "Caitlin", "Cassian", "Charles", "Rupert", "Einhardt", "Louis", "Fareesh",
            "Ingard", "Quincy", "Aslan", "Alice", "Senshi", "Jonesie", "Jackson", "Bill", "Otis"};

        firstNames.AddRange(_firstNames);

        var _lastNames = new[] { "Bastal", "Fowler", "Francis", "Tiaki", "Xulu", "Moonbeam", "Featherwing", "Lousu", "Yenwi", "Crookshank", "Ultra", "Maximus", "Smith", "Jones", "Sanada", "Zero", "Miite", "Russo", "Virilous", "Tiger", "Tanjiro", "Eoun", "Uwoni", "Hunter", "Lilywhite", "Crow", "Wisdom", "Seacastle", "Strongjaw", "Wubif"};
        lastNames.AddRange(_lastNames);

        //=====Race Data import=====
        // Load all RaceData assets from the specified Resources folder
        RaceData[] raceDataArray = Resources.LoadAll<RaceData>("RaceData");

        // If you want to clear the list each time you load to avoid duplicates
        raceDatas.Clear();

        // Add loaded RaceData assets to the list
        raceDatas.AddRange(raceDataArray);
        //==========================



    }
    
    void Start()
    {
        //========Legendary Student======
        //Initializing student data list
        remainingLegendaryStudentList = allLegendaryStudentList;
        for (var i = LegendaryStudentManager.Instance.legendaryStudentUnlockStates.Count - 1; i >= 0; i--)
        {
            Debug.Log("Checking for unlocked legendary");
            if (LegendaryStudentManager.Instance.legendaryStudentUnlockStates[i])
            {
                Debug.Log("Unlock state " + i + " is " + LegendaryStudentManager.Instance.legendaryStudentUnlockStates[i]);
                remainingLegendaryStudentList.Remove(allLegendaryStudentList[i]);  // Adjusted index here
            }
        }




        //===============================
    }

    public StudentData RandomGenerateStudent()
    {
        StudentData data = new StudentData();
        data._studentRace = raceList[Random.Range(0, raceList.Count)];
        //=======Visuals=========
        string race = data._studentRace;
        
        if (race != null/*race == "Elf" || race == "Tanuki" || race == "Gnome" || race == "Human" || race == "Wyrm" || race == "Ogre"*/)
        {
            RaceData raceData = raceDatas.Find(raceData => raceData.raceName == race);
            List<Sprite> dataSpriteList = new List<Sprite>() { data._ASprite,data._BSprite,data._CSprite,data._DSprite,data._ESprite,data._FSprite,data._GSprite};
            data._ASprite = raceData.ASpriteList[Random.Range(0, raceData.ASpriteList.Count)];
            if (raceData.BSpriteList.Count > 0)
            {
                data._BSprite = raceData.BSpriteList[Random.Range(0, raceData.BSpriteList.Count)];
            }
            data._CSprite = raceData.CSpriteList[Random.Range(0, raceData.CSpriteList.Count)];
            data._DSprite = raceData.DSpriteList[Random.Range(0, raceData.DSpriteList.Count)];
            data._ESprite = raceData.ESpriteList[Random.Range(0, raceData.ESpriteList.Count)];
            data._FSprite = raceData.FSpriteList[Random.Range(0, raceData.FSpriteList.Count)];
            data._GSprite = raceData.GSpriteList[Random.Range(0, raceData.GSpriteList.Count)];
            if (raceData.HSpriteList.Count > 0)
            {
                data._HSprite = raceData.HSpriteList[Random.Range(0, raceData.HSpriteList.Count)];
            }

            string firstName = firstNames[Random.Range(0, firstNames.Count)];
            string lastName = lastNames[Random.Range(0, lastNames.Count)];
            data._studentName = "<bounce a=0.1 f=0.3 w=1>"+firstName + " " + lastName+"</bounce>";

            //=======National Information======

            bool isLocal = (Random.Range(0, 100) < firstNationPercentage);
            if (isLocal)
            {
                data._nationType = raceData.FirstNation;                
            }
            else
            {
                data._nationType = raceData.SecondNation;
            }

            if (raceData.ISpriteList.Count > 0)
            {
                switch (data._nationType)
                {
                    case RaceData.NationType.Unmeiia:
                        data._nationality = "Unmeiia";
                        data._ISprite = raceData.ISpriteList[0];
                        break;
                    case RaceData.NationType.Nystal:
                        data._nationality = "Nystal";
                        data._ISprite = raceData.ISpriteList[1];
                        break;
                    case RaceData.NationType.Tendiyu:
                        data._nationality = "Tendiyu";
                        data._ISprite = raceData.ISpriteList[2];
                        break;
                    case RaceData.NationType.Ovyeka:
                        data._nationality = "Ovyeka";
                        data._ISprite = raceData.ISpriteList[3];
                        break;
                    case RaceData.NationType.Gessurd:
                        data._nationality = "Gessurd";
                        data._ISprite = raceData.ISpriteList[4];
                        break;
                    case RaceData.NationType.None:
                        data._nationality = " ";
                        data._ISprite = null;
                        break;
                }
            }

            Debug.Log("This student is from " + data._nationality);
        }

        //=====Legendary Student Check======
        if (GameManager.Instance.levelDataList[GameManager.Instance.currentLevelID].features.Exists(f => f.includedFeature == Feature.feature.legendaryStudents))
        {
            float ff = Random.Range(0, 100);
            if (ff < lengedaryStudentPercentage)
            {
                if (remainingLegendaryStudentList.Count > 0)
                {
                    StudentData d = remainingLegendaryStudentList[Random.Range(0, remainingLegendaryStudentList.Count)];
                    //remainingLegendaryStudentList.Remove(d);
                    data = d;
                    Debug.Log("Legendary Student Spawned!!!!");
                    return data;


                }
            }
        }

        //if not legendary studnent, set var to false
        data._legendaryStudentID = 0;

        //==================================



        //===Finance and academic====
        data._finance = Random.Range(0, 100);
        data._academic = Random.Range(0, 100);

        

        int a = Random.Range(0, 100);
        if(a < Mathf.Min(goodStudentPercentage,100))
        {
            data._academic = Random.Range(80, 100);
        }
        else if (a < Mathf.Min(badStudentPercentage + goodStudentPercentage,100))
        {
            data._academic = Random.Range(0, 20);
        }

        int f = Random.Range(0, 100);
        if (f < Mathf.Min(richStudentPercentage,100))
        {
            data._finance = Random.Range(100, 100);
        }
        else if (f < Mathf.Min(poorStudentPercentage + richStudentPercentage,100))
        {
            data._finance = Random.Range(0, 20);
        }

        if(LegendaryStudentManager.Instance.moreAcademicLessMoneyEffect)
        {
            data._finance = Mathf.Max(0,data._finance - 20);
            data._academic = Mathf.Min(100, data._academic + 40);
        }
        //============================

        //=======Personal Information======
        //==checks==
        data._isAlumni = (Random.Range(0, 100) < alumniPercentage);
        if (data._finance >= 90)
        {
            Debug.Log("this student is patron");
            data._isPatron = (Random.Range(0, 100) < patronPercentage);
        }
        data._isFirstGen = (Random.Range(0, 100) < firstGenPercentage);
        //==========


        //==bars==
        //extroversion
        float e = Random.Range(1, extroversionPercentage) * 0.025f;
        if(extroversionPercentage < 50)
        {
            e *= -1f;
        }
        data._extroversion = Mathf.Clamp(Mathf.RoundToInt(Random.Range(1, 5) + e),1,5);



        //magicalPersonality
        float m = Random.Range(1, magicalPersonalityPercentage) * 0.025f;
        if (magicalPersonalityPercentage < 50)
        {
            m *= -1f;
        }
        data._magicalPersonality = Mathf.Clamp(Mathf.RoundToInt(Random.Range(1, 5) + m), 1, 5);


        //workplace
        float w = Random.Range(1, workplacePercentage) * 0.025f;
        if (workplacePercentage < 50)
        {
            m *= -1f;
        }
        data._workplace = Mathf.Clamp(Mathf.RoundToInt(Random.Range(1, 5) + m), 1, 5);


        //schedule
        float s = Random.Range(1, schedulePercentage) * 0.025f;
        if (schedulePercentage < 50)
        {
            s *= -1f;
        }
        data._schedule = Mathf.Clamp(Mathf.RoundToInt(Random.Range(1, 5) + s), 1, 5);


        //explorativity
        float ex = Random.Range(1, explorativityPercentage) * 0.025f;
        if (explorativityPercentage < 50)
        {
            ex *= -1f;
        }
        data._explorativity = Mathf.Clamp(Mathf.RoundToInt(Random.Range(1, 5) + ex), 1, 5);

        //Psionic Affinity
        float p = Random.Range(1, psionicAffinityPercentage) * 0.025f;
        if (psionicAffinityPercentage < 50)
        {
            p  *= -1f;
        }
        data._psionicAffinity = Mathf.Clamp(Mathf.RoundToInt(Random.Range(1, 5) + m), 1, 5);
        //=======
        







        //=================================


        return data;
    }

}

