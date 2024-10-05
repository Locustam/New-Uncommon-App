using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PreferencesManager : MonoBehaviour
{
    [SerializeField] GameObject[] bars;
    [SerializeField] int[] nums;

    //private void FixedUpdate()
    //{
    //    SetBars(nums);
    //}

    public void SetBars(params int[] nums)
    {
        for(int i = 0; i < nums.Length; i++)
        {
            bars[i].GetComponent<BarRender>().SetImage(nums[i]);
        }

    }
}
