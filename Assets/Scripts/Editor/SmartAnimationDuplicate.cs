using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;

public class SmartAnimationDuplicate : EditorWindow
{
    private Animator originalAnimator;
    private AnimationClip originalAnimation;
    private string newAnimationPath = "Assets/";
    private string newAnimatorName = "";
    private Animator newAnimator;
    private string newAnimationName = "";
    private Transform newRoot;
    private Dictionary<Transform, Transform> gameObjectMapping = new Dictionary<Transform, Transform>();
    private Vector2 scrollPosition; // Scroll position

    [MenuItem("Window/Jiahao SmartAnimator/Smart Animation Duplicate")]
    public static void ShowWindow()
    {
        GetWindow<SmartAnimationDuplicate>("Smart Animation Duplicate");
    }

    private void OnGUI()
    {
        GUILayout.Label("Smart Animation Duplicate", EditorStyles.boldLabel);

        originalAnimator = (Animator)EditorGUILayout.ObjectField("Original Animator", originalAnimator, typeof(Animator), true);

        if (originalAnimator != null)
        {
            // Get all animations from the animator
            var controller = originalAnimator.runtimeAnimatorController as AnimatorController;
            if (controller != null)
            {
                AnimationClip[] clips = controller.animationClips;
                List<string> clipNames = new List<string>();
                foreach (var clip in clips)
                {
                    clipNames.Add(clip.name);
                }

                int selectedIndex = Mathf.Max(0, System.Array.IndexOf(clips, originalAnimation));
                selectedIndex = EditorGUILayout.Popup("Original Animation", selectedIndex, clipNames.ToArray());
                originalAnimation = clips[selectedIndex];
            }
        }

        newAnimationPath = EditorGUILayout.TextField("New Animation Path (from Assets/)", newAnimationPath);
        newAnimatorName = EditorGUILayout.TextField("New Animator Name (optional)", newAnimatorName);
        newAnimator = (Animator)EditorGUILayout.ObjectField("Existing Animator (optional)", newAnimator, typeof(Animator), true);
        newAnimationName = EditorGUILayout.TextField("New Animation Name", newAnimationName);
        newRoot = (Transform)EditorGUILayout.ObjectField("New Root GameObject", newRoot, typeof(Transform), true);

        if (GUILayout.Button("Auto Fill Same Names"))
        {
            AutoFillSameNames();
        }

        if (originalAnimator != null && originalAnimation != null)
        {
            GUILayout.Label("Remap GameObjects", EditorStyles.boldLabel);

            // Start ScrollView
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300)); // Adjust height as necessary

            Transform[] originalTransforms = originalAnimator.GetComponentsInChildren<Transform>();
            foreach (var transform in originalTransforms)
            {
                if (!gameObjectMapping.ContainsKey(transform))
                {
                    gameObjectMapping[transform] = null;
                }

                // Change the color based on whether the new object is set
                if (gameObjectMapping[transform] == null)
                {
                    GUI.color = Color.red; // Red if missing
                }
                else
                {
                    GUI.color = Color.green; // Green if set
                }

                // Display the object field
                gameObjectMapping[transform] = (Transform)EditorGUILayout.ObjectField(transform.name, gameObjectMapping[transform], typeof(Transform), true);

                // Reset the GUI color after each field
                GUI.color = Color.white;
            }

            // End ScrollView
            GUILayout.EndScrollView();
        }

        if (GUILayout.Button("Create"))
        {
            CreateNewAnimation();
        }
    }

    private void AutoFillSameNames()
    {
        if (newRoot == null)
        {
            Debug.LogError("Please assign a New Root GameObject.");
            return;
        }

        Transform[] originalTransforms = originalAnimator.GetComponentsInChildren<Transform>();
        Transform[] newTransforms = newRoot.GetComponentsInChildren<Transform>();

        foreach (var originalTransform in originalTransforms)
        {
            foreach (var newTransform in newTransforms)
            {
                if (originalTransform.name == newTransform.name)
                {
                    gameObjectMapping[originalTransform] = newTransform;
                }
            }
        }

        Debug.Log("Auto-fill complete based on matching names.");
    }

    private void CreateNewAnimation()
    {
        if (originalAnimation == null)
        {
            Debug.LogError("Original animation is not set.");
            return;
        }

        if (newAnimationPath == "" || newAnimationName == "")
        {
            Debug.LogError("Please set the new animation path and name.");
            return;
        }

        string fullNewAnimationPath = newAnimationPath + "/" + newAnimationName + ".anim";

        // Duplicate the original animation
        AnimationClip newClip = new AnimationClip();
        EditorUtility.CopySerialized(originalAnimation, newClip);
        AssetDatabase.CreateAsset(newClip, fullNewAnimationPath);
        AssetDatabase.SaveAssets();

        // Remap the GameObjects
        foreach (var binding in AnimationUtility.GetCurveBindings(originalAnimation))
        {
            string originalPath = binding.path;
            string newPath = RemapPath(originalPath);

            if (newPath != null)
            {
                var curve = AnimationUtility.GetEditorCurve(originalAnimation, binding);
                AnimationUtility.SetEditorCurve(newClip, new EditorCurveBinding
                {
                    path = newPath,
                    propertyName = binding.propertyName,
                    type = binding.type
                }, curve);
            }
        }

        if (newAnimator == null && !string.IsNullOrEmpty(newAnimatorName))
        {
            // Create new AnimatorController
            AnimatorController newController = AnimatorController.CreateAnimatorControllerAtPath(newAnimationPath + "/" + newAnimatorName + ".controller");
            newController.AddMotion(newClip);

            // Attach new AnimatorController to the new animator
            GameObject selectedGameObject = Selection.activeGameObject;
            if (selectedGameObject != null)
            {
                Animator animator = selectedGameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = newController;
            }
        }
        else if (newAnimator != null)
        {
            // Add the new animation to the existing animator
            var controller = newAnimator.runtimeAnimatorController as AnimatorController;
            if (controller != null)
            {
                controller.AddMotion(newClip);
            }
        }

        Debug.Log("Animation duplicated successfully!");
    }

    private string RemapPath(string originalPath)
    {
        string[] pathParts = originalPath.Split('/');
        for (int i = 0; i < pathParts.Length; i++)
        {
            foreach (var kvp in gameObjectMapping)
            {
                if (kvp.Key.name == pathParts[i] && kvp.Value != null)
                {
                    pathParts[i] = kvp.Value.name;
                }
            }
        }

        return string.Join("/", pathParts);
    }
}
