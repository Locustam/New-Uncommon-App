using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Makes it visible in the Unity inspector
public class MiniGoalData
{
    public string description;
    public string label;
    public int targetCount;
    public int currentCount;
    public bool IsCompleted;
    public int rewardType;
    public bool done = false;

    public MiniGoalData(string label, string description,int targetCount)
    {
        this.description = description;
        this.targetCount = targetCount;
        this.label = label;
        currentCount = 0;
        rewardType = Random.Range(0,2);
        Debug.Log("reward type:" + rewardType);
    }

    public void UpdateProgress(int amount = 1)
    {
        currentCount += amount;
       
        if (currentCount >= targetCount) IsCompleted = true;
        if (IsCompleted & !done)
        {
            // Optionally notify the system that this goal is completed
            Debug.Log($"Goal completed: {description}");
            //========temorarily takeout=========
            //MiniGoalManager.Instance.ReplaceMiniGoal(this); 
            done = true;

            switch(rewardType )
            {
                case 0:
                    StudentAdmissionManager.Instance.totalScholarship += 300;

                    return;

                case 1:
                    StudentAdmissionManager.Instance.studentLeft += 10;

                    return;
                
                    
                 
            }

           
        }
    }
}

