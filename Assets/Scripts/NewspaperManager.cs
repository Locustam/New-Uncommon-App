using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewsItem
{
    public string Label;
    public string Header;
    public string Description;
    public int TriggerCount;
    public int RequiredTriggerCount;
    public Sprite icon;

    

    public NewsItem(string label, string header, string description, int requiredTriggerCount)
    {
        Label = label;
        Header = header;
        Description = description;
        RequiredTriggerCount = requiredTriggerCount;
        TriggerCount = 0;
    }

    public bool IsConditionMet()
    {
        return TriggerCount >= RequiredTriggerCount;
    }
}

public class NewspaperManager : MonoBehaviour
{
    public static NewspaperManager Instance { get; private set; } // Singleton instance
    public List<NewsItem> newsItems;
    [SerializeField] private NewspaperVisuals newspaperVisuals;

        void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Keeps the instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroys any duplicate instance
        }
    }


    void Start()
    {
        InitializeNewsItems();
    }

    private void InitializeNewsItems()
    {
        //newsItems = new List<NewsItem>();
        {
            // Initialization of newsItems with an example
            // NewsItem empty = new NewsItem("emtpy", "EMPTY", " ", 1);
            // newsItems.Add(empty);
            // NewsItem scholarlyExcellenece = new NewsItem("ScholarlyExcellence", "ScholarlyExcellence!","Students are so good. Oh Yeah.", 1);
            // newsItems.Add(scholarlyExcellenece);
            // NewsItem scholarshipWho = new NewsItem("ScholarshipWho", "Scholarship who?", "by Cristina Fey \n According to our sources inside the hallowed halls of Uncommon Academy, it seems like the admissions office is pinching their proverbial pennies tighter than they ever have before. 80% of their financial aid has been left undistributed after today’s round of acceptances. Academy hopefuls beware: you may be paying out of pocket much more than you expected this year…", 1);
            // newsItems.Add(scholarshipWho);
            // NewsItem firstGenAcademy = new NewsItem("FirstGenAcademy","1ST GENERATION STUDENTS FIND A HOME AT UNCOMMON ACADEMY", "by Ipa Nemea \n Today’s Uncommon Academy admission statistics have finally been revealed, to the elation and chagrin of many applicants. UA’s campus will be full of fresh faces this year, as double the projected number of 1st generation students have been admitted! Many approve of this change in direction for the prestigious university, while some others question whether or not the school’s pedigree will remain intact. Only time will tell…",1);
            // newsItems.Add(firstGenAcademy);
        };
    }

    // Updated to increment trigger count by label
    public void IncrementTriggerCount(string label, int amount)
    {
        foreach (var newsItem in newsItems)
        {
            Debug.Log("checking " + newsItem + "for increment");
            if (newsItem.Label == label)
            {
                newsItem.TriggerCount += amount;
                Debug.Log(newsItem.Label + " successfully added and now there is: " + newsItem.TriggerCount);
                break; // Exit the loop once the matching item is found and updated
            }
        }
    }



    public List<NewsItem> SelectNews(int numberOfNewsToSelect)
    {
        List<NewsItem> metConditions = new List<NewsItem>();
        List<NewsItem> selectedNews = new List<NewsItem>();

        Debug.Log("Selecting News");
        foreach (var item in newsItems)
        {
            Debug.Log("checking item" + item.Label);
            Debug.Log(item.Label + "has " + item.TriggerCount + "and need " + item.RequiredTriggerCount);
            if (item.IsConditionMet())
            {
                Debug.Log("Met Condition: " + item.Label);
                metConditions.Add(item);
            }
        }

        for (int i = 0; i < numberOfNewsToSelect; i++)
        {
            if (metConditions.Count > 0)
            {
                int randomIndex = Random.Range(0, metConditions.Count);
                selectedNews.Add(metConditions[randomIndex]);
                metConditions.RemoveAt(randomIndex);
                Debug.Log("added" + selectedNews);
            }
            else
            {
                // Add an "empty" news item if there aren't enough conditions met
                selectedNews.Add(new NewsItem("NoNews", "", "", 0));
            }
        }

        newspaperVisuals.UpdateVisuals(selectedNews[0],selectedNews[1],selectedNews[2]);
        Debug.Log(selectedNews[0].Label + selectedNews[1].Label + selectedNews[2].Label);
        return selectedNews;
    }

    public void CheckAllNewsEndings()
    {
        //if (GameManager.Instance.inGame)
        {
            if (StudentAdmissionManager.Instance.averageAcademic >= StudentAdmissionManager.Instance.academicMidValue * 2 * 0.8f)
            {
                Debug.Log("scholarshipexcellence increased");
                IncrementTriggerCount("ScholarlyExcellence", 1);
            }

            if (StudentAdmissionManager.Instance.totalScholarship >= StudentAdmissionManager.Instance.initialScholarship*0.3f)
            {
                Debug.Log("totalScholarship");
                IncrementTriggerCount("ScholarshipWho", 1);
            }
            
            if (StudentAdmissionManager.Instance.firstGenStudent >= 5f)
            {
                Debug.Log("firstGenAcademy");
                IncrementTriggerCount("FirstGenAcademy", 1);
            }

            if(StudentAdmissionManager.Instance.alumniStudent >= 5f)
            {
                Debug.Log("AlumniAcademy");
                IncrementTriggerCount("AlumniAcademy", 1);

            }

            if(StudentAdmissionManager.Instance.patronStudent >= 5f)
            {
                Debug.Log("PatronAcademy");
                IncrementTriggerCount("PatronAcademy", 1);

            }


        }
    }
}
