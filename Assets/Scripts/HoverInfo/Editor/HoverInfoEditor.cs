using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HoverInfoManager))]
public class HoverInfoManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the reference to the HoverInfoManager
        HoverInfoManager manager = (HoverInfoManager)target;

        // Draw the default inspector fields
        DrawDefaultInspector();

        // Add a button for finding all Hoverinfo objects in the scene
        if (GUILayout.Button("Find All HoverInfo Objects"))
        {
            manager.FindAllHoverInfoObjects();
        }

        // Add a button for updating the hover info text for all objects
        if (GUILayout.Button("Update HoverInfo Texts"))
        {
            manager.UpdateHoverInfoTexts();
        }

        // Add a button for updating all hover info components' variables
        if (GUILayout.Button("Update HoverInfo Components"))
        {
            manager.UpdateHoverInfoComponents();
        }
    }
}
