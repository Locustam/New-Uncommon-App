using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  // Draws the default inspector

        LevelManager script = (LevelManager)target;  // Reference to the script being edited

        // Create a button in the inspector
        if (GUILayout.Button("Set Level to LevelNumberToSet"))
        {
            script.SetCurrentLevel(script.levelNumberToSet);  // Call the function with a parameter
        }
    }
}
