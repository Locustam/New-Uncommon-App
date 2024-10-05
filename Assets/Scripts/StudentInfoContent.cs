using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentInfoContent : MonoBehaviour
{
    public StudentData data;
    public Image studentImage;
    public TextMeshProUGUI studentNameText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateContent();
    }

    void UpdateContent()
    {
        studentNameText.text = data._studentName;
    }
}
