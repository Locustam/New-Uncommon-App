using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoManager : MonoBehaviour
{
    public List<Hoverinfo> hoverInfoObjects = new List<Hoverinfo>();
    public HoverInfoData hoverInfoData;

    // New variables to update hover info components
    public Sprite backgroundSprite;    // The background sprite for the hover info
    public GameObject hoverInfoPrefab; // Prefab for the hover info
    public float hoverDelay = 1.0f;    // Time in seconds required to hover before showing the hover UI
    public float hoverOffsetX = 10f;   // X offset for hover UI
    public float hoverOffsetY = -10f;  // Y offset for hover UI

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

    // Updates the hover info components with the manager's settings
    public void UpdateHoverInfoComponents()
    {
        foreach (Hoverinfo hoverInfo in hoverInfoObjects)
        {
            hoverInfo.hoverPrefab = hoverInfoPrefab;
            hoverInfo.hoverDelay = hoverDelay;
            hoverInfo.offset = new Vector2(hoverOffsetX, hoverOffsetY);

            if (hoverInfo.GetComponent<Image>() != null)
            {
                hoverInfo.GetComponent<Image>().sprite = backgroundSprite;
            }
        }
    }
}
