using UnityEngine;
using UnityEngine.EventSystems; // Required for event interfaces

public class HoverAnimationTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;
    public string onHoverAnimationName;
    public string offHoverAnimationName;
    public enum ParameterType { Boolean, Trigger, Integer, FloatVar }
    public ParameterType thisParameterType;
    
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            // Ensure there is an animator component
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on the GameObject!");
            }
        }
    }

    // Called when the mouse enters the UI element's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (thisParameterType)
        {
            case ParameterType.Boolean:
                animator.SetBool(onHoverAnimationName, true);
                break;
            case ParameterType.Trigger:
                animator.SetTrigger(onHoverAnimationName);
                break;
            case ParameterType.Integer:
                animator.SetInteger(onHoverAnimationName, 1);
                break;
            case ParameterType.FloatVar:
                animator.SetFloat(onHoverAnimationName, 1.0f);
                break;
        }
    }

    // Called when the mouse exits the UI element's area
    public void OnPointerExit(PointerEventData eventData)
    {
        switch (thisParameterType)
        {
            case ParameterType.Boolean:
                animator.SetBool(onHoverAnimationName, false);
                break;
            case ParameterType.Trigger:
                // Triggers can't be unset, so we might need to adjust logic if using a trigger for off-hover
                animator.SetTrigger(offHoverAnimationName);
                break;
            case ParameterType.Integer:
                animator.SetInteger(offHoverAnimationName, 0);
                break;
            case ParameterType.FloatVar:
                animator.SetFloat(offHoverAnimationName, 0.0f);
                break;
        }
    }
}
