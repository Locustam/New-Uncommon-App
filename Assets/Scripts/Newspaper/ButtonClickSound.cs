using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public void ButtonClickSoundOK()
    {
        SoundManager.Instance.PlaySFX("Click_OK");
    }
}
