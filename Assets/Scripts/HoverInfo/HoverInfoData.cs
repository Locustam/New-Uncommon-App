using UnityEngine;

[CreateAssetMenu(fileName = "HoverInfoData", menuName = "HoverInfo/HoverInfoData", order = 1)]
public class HoverInfoData : ScriptableObject
{
    [System.Serializable]
    public class HoverInfoEntry
    {
        public string HoverInfoID;
        public string HoverText;
    }

    public HoverInfoEntry[] hoverInfoEntries;
}
