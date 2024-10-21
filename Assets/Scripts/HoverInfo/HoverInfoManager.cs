using System.Collections.Generic;
using UnityEngine;

public class HoverInfoManager : MonoBehaviour
{
    public List<Hoverinfo> hoverInfoObjects = new List<Hoverinfo>();
    public HoverInfoData hoverInfoData;

    // Finds all objects with Hoverinfo component in the scene and adds them to the list
    public void FindAllHoverInfoObjects()
    {
        hoverInfoObjects.Clear();  // Clear the current list
        Hoverinfo[] foundObjects = FindObjectsOfType<Hoverinfo>();

        foreach (Hoverinfo hoverInfo in foundObjects)
        {
            hoverInfoObjects.Add(hoverInfo);
        }
    }

    // Updates all hover info text based on the HoverInfoData ScriptableObject
    public void UpdateHoverInfoTexts()
    {
        if (hoverInfoData == null)
        {
            Debug.LogError("HoverInfoData is not assigned!");
            return;
        }

        // Iterate through all Hoverinfo objects and update their text based on matching HoverInfoID
        foreach (Hoverinfo hoverInfo in hoverInfoObjects)
        {
            foreach (HoverInfoData.HoverInfoEntry entry in hoverInfoData.hoverInfoEntries)
            {
                if (hoverInfo.hoverInfoID == entry.HoverInfoID)
                {
                    hoverInfo.hoverText = entry.HoverText;
                    break;
                }
            }
        }
    }
}
