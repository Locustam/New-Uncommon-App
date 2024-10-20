#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SmartAnimationEditor : EditorWindow
{
    private enum Mode { EditSpecificFrame, EditRangeAnchor }

    private Animator animatorObject;
    private AnimationClip selectedClip;
    private List<AnimationClip> animationClips;
    private int frame = 0;
    private int startRangeFrame = 0;
    private int endRangeFrame = 0;
    private float frameTime = 0;
    private List<EditorCurveBinding> curveBindings;
    private Mode currentMode = Mode.EditSpecificFrame;
    private Vector2 scrollPosition; // Add scroll position

    [MenuItem("Window/Jiahao SmartAnimator/Smart Animation Editor")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<SmartAnimationEditor>("Smart Animation Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Smart Animation Editor", EditorStyles.boldLabel);

        // Drag and drop Animator field
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Referenced Animator:", GUILayout.Width(150));

        Animator newAnimatorObject = (Animator)EditorGUILayout.ObjectField(
            animatorObject,
            typeof(Animator),
            true,
            GUILayout.Width(300)
        );

        // If Animator changes, update the animation clips list
        if (newAnimatorObject != animatorObject)
        {
            animatorObject = newAnimatorObject;
            UpdateAnimationClips();
        }

        EditorGUILayout.EndHorizontal();

        // Ensure an Animator is selected and contains animation clips
        if (animatorObject != null && animationClips.Count > 0)
        {
            // Dropdown to select an animation clip
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Animation Clip:", GUILayout.Width(150));

            int selectedIndex = animationClips.IndexOf(selectedClip);
            selectedIndex = EditorGUILayout.Popup(selectedIndex, animationClips.Select(clip => clip.name).ToArray(), GUILayout.Width(300));

            // Update selected clip when a new one is chosen
            if (selectedIndex >= 0 && selectedClip != animationClips[selectedIndex])
            {
                selectedClip = animationClips[selectedIndex];
            }

            EditorGUILayout.EndHorizontal();

            // Mode Selection
            currentMode = (Mode)EditorGUILayout.EnumPopup("Mode", currentMode);

            // Start of scrollable content
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            if (currentMode == Mode.EditSpecificFrame)
            {
                // Edit Specific Frame Mode
                GUILayout.Space(10);
                GUILayout.Label("Edit Specific Frame", EditorStyles.boldLabel);
                GUILayout.Label("This mode allows you to edit keyframes at a specific frame and update their values to match the current scene state.");

                frame = EditorGUILayout.IntField("Frame", frame);

                if (GUILayout.Button("Get Keyframes"))
                {
                    GetKeyframesAtFrame();
                }

                if (curveBindings != null && curveBindings.Count > 0)
                {
                    DisplayKeyframes();
                }
            }
            else if (currentMode == Mode.EditRangeAnchor)
            {
                // Edit Range Anchor Mode
                GUILayout.Space(10);
                GUILayout.Label("Edit Range Anchor", EditorStyles.boldLabel);
                GUILayout.Label("This mode allows you to edit keyframes over a range of frames, adjusting them relative to the current state of the element.");

                startRangeFrame = EditorGUILayout.IntField("Start Range Frame", startRangeFrame);
                endRangeFrame = EditorGUILayout.IntField("End Range Frame", endRangeFrame);

                if (GUILayout.Button("Get Keyframes for Range Anchor"))
                {
                    GetKeyframesAtFrame(startRangeFrame);
                }

                if (curveBindings != null && curveBindings.Count > 0)
                {
                    DisplayRangeKeyframes();
                }
            }

            // End of scrollable content
            EditorGUILayout.EndScrollView();
        }
        else
        {
            GUILayout.Label("select an animator with animation clips");
        }
    }

    private void GetKeyframesAtFrame()
    {
        // Clear previous data
        curveBindings = new List<EditorCurveBinding>();

        if (selectedClip == null) return;

        // Calculate the time corresponding to the given frame
        frameTime = frame / selectedClip.frameRate;

        // Get all the curve bindings (transform and rect transform properties)
        var curves = AnimationUtility.GetCurveBindings(selectedClip);

        foreach (var curve in curves)
        {
            // Filter out unsupported types and only process Transform and RectTransform properties
            if (SupportsBinding(curve))
            {
                var keyframes = AnimationUtility.GetEditorCurve(selectedClip, curve)?.keys;

                // Add the curve if it has a keyframe at the current frame time
                if (keyframes != null)
                {
                    foreach (var keyframe in keyframes)
                    {
                        if (Mathf.Approximately(keyframe.time, frameTime))
                        {
                            curveBindings.Add(curve);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void GetKeyframesAtFrame(int startFrame)
    {
        // Clear previous data
        curveBindings = new List<EditorCurveBinding>();

        if (selectedClip == null) return;

        // Calculate the time corresponding to the start frame
        frameTime = startFrame / selectedClip.frameRate;

        // Get all the curve bindings (transform and rect transform properties)
        var curves = AnimationUtility.GetCurveBindings(selectedClip);

        foreach (var curve in curves)
        {
            // Filter out unsupported types and only process Transform and RectTransform properties
            if (SupportsBinding(curve))
            {
                var keyframes = AnimationUtility.GetEditorCurve(selectedClip, curve)?.keys;

                // Add the curve if it has a keyframe at the start frame time
                if (keyframes != null)
                {
                    foreach (var keyframe in keyframes)
                    {
                        if (Mathf.Approximately(keyframe.time, frameTime))
                        {
                            curveBindings.Add(curve);
                            break;
                        }
                    }
                }
            }
        }
    }

    private bool SupportsBinding(EditorCurveBinding binding)
    {
        // Only allow Transform and RectTransform properties
        return binding.type == typeof(Transform) || binding.type == typeof(RectTransform);
    }

    private void DisplayKeyframes()
    {
        GUILayout.Label($"Keyframes at Frame {frame}:", EditorStyles.boldLabel);

        for (int i = 0; i < curveBindings.Count; i++)
        {
            EditorCurveBinding binding = curveBindings[i];

            GUILayout.BeginHorizontal();

            // Display the path and property name of the keyframe
            GUILayout.Label($"{binding.path} - {binding.propertyName}", GUILayout.Width(300));

            if (GUILayout.Button("Set based on current", GUILayout.Width(150)))
            {
                SetKeyframeToCurrentState(binding);
            }

            GUILayout.EndHorizontal();
        }
    }

    private void DisplayRangeKeyframes()
    {
        GUILayout.Label($"Keyframes at Start Range Frame {startRangeFrame}:", EditorStyles.boldLabel);

        for (int i = 0; i < curveBindings.Count; i++)
        {
            EditorCurveBinding binding = curveBindings[i];

            GUILayout.BeginHorizontal();

            // Display the path and property name of the keyframe
            GUILayout.Label($"{binding.path} - {binding.propertyName}", GUILayout.Width(300));

            if (GUILayout.Button("Set range anchor based on current", GUILayout.Width(200)))
            {
                SetRangeAnchorBasedOnCurrent(binding);
            }

            GUILayout.EndHorizontal();
        }
    }

    private void SetKeyframeToCurrentState(EditorCurveBinding binding)
    {
        if (animatorObject == null) return;

        Transform targetTransform = animatorObject.transform.Find(binding.path);

        if (targetTransform == null)
        {
            Debug.LogWarning($"No object found at path: {binding.path}");
            return;
        }

        // Handle different types of keyframes (position, rotation, scale, RectTransform properties)
        AnimationCurve curve = AnimationUtility.GetEditorCurve(selectedClip, binding);
        if (curve != null)
        {
            Keyframe[] keyframes = curve.keys;

            if (binding.propertyName.Contains("m_LocalPosition"))
            {
                Vector3 position = targetTransform.localPosition;

                SetKeyframeValue(binding, position, keyframes);
            }
            else if (binding.propertyName.Contains("m_LocalRotation"))
            {
                Quaternion rotation = targetTransform.localRotation;

                SetKeyframeValue(binding, rotation.eulerAngles, keyframes);
            }
            else if (binding.propertyName.Contains("m_LocalScale"))
            {
                Vector3 scale = targetTransform.localScale;

                SetKeyframeValue(binding, scale, keyframes);
            }
            else if (binding.propertyName.Contains("m_AnchoredPosition"))
            {
                RectTransform rectTransform = targetTransform as RectTransform;

                if (rectTransform != null)
                {
                    SetKeyframeValue(binding, rectTransform.anchoredPosition, keyframes);
                }
            }
        }

        // Refresh the animation curve
        AnimationUtility.SetEditorCurve(selectedClip, binding, curve);
        Debug.Log($"Updated keyframe for {binding.path} - {binding.propertyName}");
    }

    private void SetRangeAnchorBasedOnCurrent(EditorCurveBinding binding)
    {
        if (animatorObject == null) return;

        Transform targetTransform = animatorObject.transform.Find(binding.path);

        if (targetTransform == null)
        {
            Debug.LogWarning($"No object found at path: {binding.path}");
            return;
        }

        // Handle different types of keyframes
        AnimationCurve curve = AnimationUtility.GetEditorCurve(selectedClip, binding);
        if (curve == null) return;

        Keyframe[] keyframes = curve.keys;
        Keyframe startFrameKey = default;

        for (int i = 0; i < keyframes.Length; i++)
        {
            if (Mathf.Approximately(keyframes[i].time, frameTime))
            {
                startFrameKey = keyframes[i];
                break;
            }
        }

        // Calculate the difference between the new and old start frame value
        float newValue = GetCurrentValueForBinding(binding, targetTransform);
        float valueDiff = newValue - startFrameKey.value;

        // Update all keyframes in the range
        for (int i = 0; i < keyframes.Length; i++)
        {
            float currentFrame = keyframes[i].time * selectedClip.frameRate;

            if (currentFrame >= startRangeFrame && currentFrame <= endRangeFrame)
            {
                keyframes[i].value += valueDiff;
                curve.MoveKey(i, keyframes[i]);
            }
        }

        // Save the modified curve
        AnimationUtility.SetEditorCurve(selectedClip, binding, curve);
    }

    private float GetCurrentValueForBinding(EditorCurveBinding binding, Transform targetTransform)
    {
        if (binding.propertyName.Contains("m_LocalPosition.x"))
        {
            return targetTransform.localPosition.x;
        }
        else if (binding.propertyName.Contains("m_LocalPosition.y"))
        {
            return targetTransform.localPosition.y;
        }
        else if (binding.propertyName.Contains("m_LocalPosition.z"))
        {
            return targetTransform.localPosition.z;
        }
        else if (binding.propertyName.Contains("m_LocalRotation.x"))
        {
            return targetTransform.localRotation.eulerAngles.x;
        }
        else if (binding.propertyName.Contains("m_LocalRotation.y"))
        {
            return targetTransform.localRotation.eulerAngles.y;
        }
        else if (binding.propertyName.Contains("m_LocalRotation.z"))
        {
            return targetTransform.localRotation.eulerAngles.z;
        }
        else if (binding.propertyName.Contains("m_LocalScale.x"))
        {
            return targetTransform.localScale.x;
        }
        else if (binding.propertyName.Contains("m_LocalScale.y"))
        {
            return targetTransform.localScale.y;
        }
        else if (binding.propertyName.Contains("m_LocalScale.z"))
        {
            return targetTransform.localScale.z;
        }
        else if (binding.propertyName.Contains("m_AnchoredPosition.x"))
        {
            return (targetTransform as RectTransform)?.anchoredPosition.x ?? 0;
        }
        else if (binding.propertyName.Contains("m_AnchoredPosition.y"))
        {
            return (targetTransform as RectTransform)?.anchoredPosition.y ?? 0;
        }

        return 0;
    }

    private void SetKeyframeValue(EditorCurveBinding binding, Vector3 newValue, Keyframe[] keyframes)
    {
        AnimationCurve curve = AnimationUtility.GetEditorCurve(selectedClip, binding);
        if (curve == null) return;

        // Update keyframe values at the specified frame time
        for (int i = 0; i < keyframes.Length; i++)
        {
            if (Mathf.Approximately(keyframes[i].time, frameTime))
            {
                if (binding.propertyName.Contains("m_LocalPosition.x") || binding.propertyName.Contains("m_AnchoredPosition.x"))
                {
                    keyframes[i].value = newValue.x;
                }
                else if (binding.propertyName.Contains("m_LocalPosition.y") || binding.propertyName.Contains("m_AnchoredPosition.y"))
                {
                    keyframes[i].value = newValue.y;
                }
                else if (binding.propertyName.Contains("m_LocalPosition.z"))
                {
                    keyframes[i].value = newValue.z;
                }
                else if (binding.propertyName.Contains("m_LocalRotation.x"))
                {
                    keyframes[i].value = newValue.x;
                }
                else if (binding.propertyName.Contains("m_LocalRotation.y"))
                {
                    keyframes[i].value = newValue.y;
                }
                else if (binding.propertyName.Contains("m_LocalRotation.z"))
                {
                    keyframes[i].value = newValue.z;
                }
                else if (binding.propertyName.Contains("m_LocalScale.x"))
                {
                    keyframes[i].value = newValue.x;
                }
                else if (binding.propertyName.Contains("m_LocalScale.y"))
                {
                    keyframes[i].value = newValue.y;
                }
                else if (binding.propertyName.Contains("m_LocalScale.z"))
                {
                    keyframes[i].value = newValue.z;
                }

                // Apply the changes
                curve.MoveKey(i, keyframes[i]);
            }
        }

        // Save the modified curve
        AnimationUtility.SetEditorCurve(selectedClip, binding, curve);
    }

    private void UpdateAnimationClips()
    {
        if (animatorObject != null)
        {
            animationClips = new List<AnimationClip>();

            // Get all animation clips from the animator's runtime controller
            if (animatorObject.runtimeAnimatorController != null)
            {
                animationClips.AddRange(animatorObject.runtimeAnimatorController.animationClips);
            }
        }
        else
        {
            animationClips.Clear();
        }
    }
}

#endif
