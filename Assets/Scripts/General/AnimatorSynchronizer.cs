using UnityEngine;

public class AnimatorSynchronizer : MonoBehaviour
{
    public Animator sourceAnimator;
    public Animator targetAnimator;

    void Update()
    {
        // Get the current state info from the source animator
        AnimatorStateInfo sourceStateInfo = sourceAnimator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = sourceStateInfo.normalizedTime;

        // Set the target animator's state to match the source
        targetAnimator.Play(sourceAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, normalizedTime);
    }
}
