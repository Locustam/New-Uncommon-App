using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncementManager : MonoBehaviour
{
    public static AnnouncementManager Instance { get; private set; }

    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI text;

    void Awake()
    {
        // Check if an instance already exists and if it's not the current one, destroy it
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void DisplayAnnouncement(string content)
    {
        if (content != null)
        {
            text.text = content;
            animator.SetTrigger("Display");
        }
    }
}
