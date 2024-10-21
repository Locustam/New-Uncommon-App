using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

[CustomEditor(typeof(MonoBehaviour), true)]
public class PublicFunctionInvokerEditor : Editor
{
    // Store all public methods of the target object
    private List<MethodInfo> publicMethods = new List<MethodInfo>();
    private int selectedMethodIndex = 0;  // Index of the currently selected method from the dropdown
    private bool showFunctionDropdown = false;  // Toggles the visibility of the function dropdown

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        MonoBehaviour targetMono = (MonoBehaviour)target;

        // Create a horizontal layout for the buttons
        EditorGUILayout.BeginHorizontal();

        // Make the "Add Public Function Invoke" button smaller
        if (GUILayout.Button("Add Public Function Invoke", GUILayout.MaxWidth(160)))
        {
            // Toggle the function dropdown visibility
            showFunctionDropdown = !showFunctionDropdown;

            // Fetch all public methods from the target script if not already fetched
            if (showFunctionDropdown)
            {
                FetchPublicMethods(targetMono);
            }
        }

        // Make the "Remove Invoke" button smaller and aligned
        if (GUILayout.Button("Remove Invoke", GUILayout.MaxWidth(100)))
        {
            showFunctionDropdown = false;
            publicMethods.Clear();
        }

        EditorGUILayout.EndHorizontal();

        // Display the function dropdown if toggled
        if (showFunctionDropdown && publicMethods.Count > 0)
        {
            // Create a dropdown for the public functions
            string[] methodNames = GetMethodNames();
            selectedMethodIndex = EditorGUILayout.Popup("Select Method", selectedMethodIndex, methodNames);

            // Display the buttons to invoke the selected method
            EditorGUILayout.BeginHorizontal();

            // Make invoke button smaller
            if (GUILayout.Button("Invoke", GUILayout.MaxWidth(80)))
            {
                InvokeSelectedMethod(targetMono, publicMethods[selectedMethodIndex]);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    // Fetch all public methods from the target MonoBehaviour
    private void FetchPublicMethods(MonoBehaviour target)
    {
        publicMethods.Clear();  // Clear any previous methods

        // Get all public instance methods declared in the script
        MethodInfo[] methods = target.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        // Add the public methods to the list
        foreach (MethodInfo method in methods)
        {
            // Only add methods that have no parameters for simplicity
            if (method.GetParameters().Length == 0)
            {
                publicMethods.Add(method);
            }
        }

        // Reset the selected index if needed
        if (publicMethods.Count > 0)
        {
            selectedMethodIndex = 0;
        }
    }

    // Get the names of all public methods
    private string[] GetMethodNames()
    {
        string[] methodNames = new string[publicMethods.Count];
        for (int i = 0; i < publicMethods.Count; i++)
        {
            methodNames[i] = publicMethods[i].Name;
        }
        return methodNames;
    }

    // Invoke the selected method on the target MonoBehaviour
    private void InvokeSelectedMethod(MonoBehaviour target, MethodInfo method)
    {
        if (method != null)
        {
            method.Invoke(target, null);  // Invoke the method without parameters
            Debug.Log($"Invoked {method.Name} on {target.name}");
        }
        else
        {
            Debug.LogError("Selected method is null!");
        }
    }
}
