using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFormater : MonoBehaviour
{
    public StudentInfo studentInfo;
    public StudentData targetData;
    [SerializeField] TextMeshProUGUI intro, extro, calm, emotion, night, early, care, explore, non, psychic;
    [SerializeField] Color Yes, No;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Determine()
    {
        targetData = studentInfo.data;
        if (targetData != null) 
        {
            Debug.Log("extroversion is" + targetData._extroversion.ToString());
            if(targetData._extroversion < 3)
            {
                intro.text = "<incr a=0.8 f=1 w=1>Introverted</incr>";
                intro.color = Yes;
                extro.text = "<incr a=0.8 f=1 w=1></incr>Extroverted";
                extro.color = No;

            }
            if(targetData._extroversion > 3)
            {
                intro.text = "<incr a=0.8 f=1 w=1></incr>Introverted";
                intro.color = No;
                extro.text = "<incr a=0.8 f=1 w=1>Extroverted</incr>";
                extro.color = Yes;
            }
            if( targetData._extroversion == 3)
            {
                intro.text = "<incr a=1 f=1 w=1></incr>Introverted";
                intro.color = No;
                extro.text = "<incr a=1 f=1 w=1></incr>Extroverted";
                extro.color = No;
            }   
            if(targetData._magicalPersonality < 3)
            {
                calm.text = "<incr a=0.8 f=1 w=1>Calm</incr>";
                calm.color = Yes;
                emotion.text = "<incr a=0.8 f=1 w=1></incr>Emotional";
                emotion.color = No;
            }
            if (targetData._magicalPersonality > 3)
            {
                calm.text = "<incr a=0.8 f=1 w=1></incr>Calm";
                calm.color = No;
                emotion.text = "<incr a=0.8 f=1 w=1>Emotional</incr>";
                emotion.color = Yes;
            }
            if(targetData._magicalPersonality == 3)
            {
                calm.text = "<incr a=1 f=1 w=1></incr>Calm";
                calm.color = No;
                emotion.text = "<incr a=1 f=1 w=1></incr>Emotional";
                emotion.color = No;
            }
            if (targetData._schedule < 3)
            {
                night.text = "<incr a=0.8 f=1 w=1>Night Owl</incr>";
                night.color = Yes;
                early.text = "<incr a=0.8 f=1 w=1></incr>Early Bird";
                early.color = No;
            }
            if (targetData._schedule > 3)
            {
                night.text = "<incr a=0.8 f=1 w=1></incr>Night Owl";
                night.color = No;
                early.text = "<incr a=0.8 f=1 w=1>Early Bird</incr>";
                early.color = Yes;
            }
            if(targetData._schedule == 3)
            {
                night.text = "<incr a=1 f=1 w=1></incr>Night Owl";
                night.color = No;
                early.text = "<incr a=1 f=1 w=1></incr>Early Bird";
                early.color = No;
            }
            if (targetData._explorativity < 3)
            {
                care.text = "<incr a=0.8 f=1 w=1>Careful</incr>";
                care.color = Yes;
                explore.text = "<incr a=0.8 f=1 w=1></incr>Explorative";
                explore.color = No;
            }
            if (targetData._explorativity > 3)
            {
                care.text = "<incr a=0.8 f=1 w=1></incr>Careful";
                care.color = No;
                explore.text = "<incr a=0.8 f=1 w=1>Explorative</incr>";
                explore.color = Yes;
            }
            if(targetData._explorativity == 3)
            {
                care.text = "<incr a=1 f=1 w=1></incr>Careful";
                care.color = No;
                explore.text = "<incr a=1 f=1 w=1></incr>Explorative";
                explore.color = No;
            }
            if (targetData._psionicAffinity < 3)
            {
                non.text = "<incr a=0.8 f=1 w=1>Non-psychic</incr>";
                non.color = Yes;
                psychic.text = "<incr a=0.8 f=1 w=1></incr>Psychic";
                psychic.color = No;
            }
            if (targetData._psionicAffinity > 3)
            {
                non.text = "<incr a=0.8 f=1 w=1></incr>Non-psychic";
                non.color = No;
                psychic.text = "<incr a=0.8 f=1 w=1>Psychic</incr>";
                psychic.color = Yes;
            }
            if(targetData._psionicAffinity == 3)
            {
                non.text = "<incr a=1 f=1 w=1></incr>Non-psychic";
                non.color = No;
                psychic.text = "<incr a=1 f=1 w=1></incr>Psychic";
                psychic.color = No;
            }
        }
    }
}
