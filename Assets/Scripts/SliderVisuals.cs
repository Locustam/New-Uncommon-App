using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.UI;

public class SliderVisuals : MonoBehaviour
{

    public List<Color> colorList = new List<Color>();
    public List<int> threshHolds = new List<int>();

    [SerializeField] Image fillImage;

    private void Start()
    {
        UpdateSliderColor();
    }

    public void UpdateSliderColor()
    {
        var value = GetComponent<Slider>().value;
        Color color = colorList[0];
        for(int i = 0; i < threshHolds.Count; i++)
        {
            if(value > threshHolds[i])
            {
                color = colorList[i];
            }
            else
            {
                break;
            }
        }

        fillImage.color = color;

    }


}
