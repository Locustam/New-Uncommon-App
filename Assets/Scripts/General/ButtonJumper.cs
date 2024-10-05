using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJumper : MonoBehaviour
{
    private Animator animator;
    private Button button;
    private bool hasClicked = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    public void Stop()
    {
        hasClicked = true;
        animator.SetBool("isPlaying", false);
        //animator.enabled = false;
    }

    public void NewMessageSound()
    {
        SoundManager.Instance.PlaySFX("NewMessage");
    }
  
}
