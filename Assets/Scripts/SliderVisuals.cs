using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.UI;

public class SliderVisuals : MonoBehaviour
{
    
    public List<Color> colorList = new List<Color>();
    [Header("Note: The value below could be managed procedurally in code")]
    public List<int> threshHolds = new List<int>();
    public List<float> percentageThreshHolds = new List<float>();

    [SerializeField] Image fillImage;

    [Header("If a danger line indcation is needed, set a slider here:")]
    [SerializeField] Slider dangerLineSlider;

    private void Start()
    {
        UpdateSliderColor();
    }

    public void UpdateSliderColor()
    {
        var value = GetComponent<Slider>().value;
        if (value != null)
        {
            Color color = colorList[0];
            for (int i = 0; i < threshHolds.Count; i++)
            {
                if (value > threshHolds[i])
                {
                    color = colorList[i];
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < percentageThreshHolds.Count; i++)
            {
                if (value/GetComponent<Slider>().maxValue > percentageThreshHolds[i])
                {
                    color = colorList[i];
                }
                else
                {
                    break;
                }
            }


            fillImage.color = color;
            if (dangerLineSlider != null)
            {
                dangerLineSlider.value = threshHolds[1]+1;
                dangerLineSlider.maxValue = GetComponent<Slider>().maxValue;
            }
        }

    }


}
