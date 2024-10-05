using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainSound : MonoBehaviour
{
    public void Opensound()
    {
        SoundManager.Instance.PlaySFX("Curtain Open");
    }
    // Start is called before the first frame update
    public void Closesound()
    {
        SoundManager.Instance.PlaySFX("Curtain Close");
    }
}
