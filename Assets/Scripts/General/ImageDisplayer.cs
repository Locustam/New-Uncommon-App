using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplayer : MonoBehaviour
{
    public static ImageDisplayer Instance { get; private set; } // Singleton instance
    
    public Image imageToDisplay;
    public GameObject backgroundLayer;
    public GameObject ImageDisplayerObject;
    bool isImageDisplayed = false;


    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this; // Assign the current instance
            DontDestroyOnLoad(gameObject); // Make it persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance by destroying any duplicates
        }
    }

    public void ToggleImageDisplayer(Sprite displayedSprite)
    {   
        if(!isImageDisplayed)
        {
            isImageDisplayed = true;
            imageToDisplay.sprite = displayedSprite;
            backgroundLayer.SetActive(true);
            ImageDisplayerObject.SetActive(true);
        }else
        {
            isImageDisplayed = false;
            backgroundLayer.SetActive(false);
            ImageDisplayerObject.SetActive(false);

        }



    }
    

}
