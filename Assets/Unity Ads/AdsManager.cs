using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    public static AdsManager Instance { get; private set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        bannerAds.Invoke("LoadBannerAd",1f);

        interstitialAds.Invoke("LoadInterstitialAd", 1f);
        rewardedAds.Invoke("LoadRewardedAd",1f);
        Debug.Log("ads loaded");
    }
}
