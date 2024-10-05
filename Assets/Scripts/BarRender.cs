using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarRender : MonoBehaviour
{
    public Image oldImage;
    [SerializeField] Sprite[] images;
    public int input;

    private void Start()
    {
        SetImage(input);
    }

    public void SetImage(int num)
    {
        if (num > 5 || num < 0)
        {
            Debug.Log("scale does not go higher than 5 or lower than 0");
        } else
        {
            Sprite newSprite = images[num];
            oldImage.sprite = newSprite;
        }
    }
}
