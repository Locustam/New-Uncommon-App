using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewspaperVisuals : MonoBehaviour
{
    [SerializeField] Image newsOneImage;
    [SerializeField] Image newsTwoImage;
    [SerializeField] Image titleNewsImage;

    [SerializeField] TextMeshProUGUI newsOneTitle;
    [SerializeField] TextMeshProUGUI newsTwoTitle;
    [SerializeField] TextMeshProUGUI titleNewsTitle;
    [SerializeField] TextMeshProUGUI newsOneContent;
    [SerializeField] TextMeshProUGUI newsTwoContent;
    [SerializeField] TextMeshProUGUI titleNewsContent;
    [SerializeField] GameObject newsOneNoData;
    [SerializeField] GameObject newsTwoNoData;
    [SerializeField] GameObject titleNewsNoData;


    public void UpdateVisuals(NewsItem newsItem1, NewsItem newsItem2, NewsItem titleNews)
    {       
        newsOneNoData.SetActive(false);
        newsOneImage.sprite = newsItem1.icon;
        newsOneTitle.text = newsItem1.Header;
        newsOneContent.text = newsItem1.Description;
        if(newsItem1.Label== "NoNews")
        {
            newsOneNoData.SetActive(true);

        }else{
            newsOneNoData.SetActive(false);
        }

        newsOneNoData.SetActive(false);
        newsTwoImage.sprite = newsItem2.icon;
        newsTwoTitle.text = newsItem2.Header;
        newsTwoContent.text = newsItem2.Description;
        if(newsItem2.Label== "NoNews")
        {
            newsTwoNoData.SetActive(true);

        }else{
            newsTwoNoData.SetActive(false);
        }

        titleNewsNoData.SetActive(false);
        titleNewsImage.sprite = titleNews.icon;
        titleNewsTitle.text = titleNews.Header;
        titleNewsContent.text = titleNews.Description;
        if(titleNews.Label== "NoNews")
        {
            titleNewsNoData.SetActive(true);
        }else{
            titleNewsNoData.SetActive(false);
        }
    }
    public void PaperSound()
    {
        SoundManager.Instance.PlaySFX("NewPaper");
    }
}
