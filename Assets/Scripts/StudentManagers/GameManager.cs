using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public bool inGame = true;
    public bool win = false;
    [SerializeField] GameObject gameLoseScreen;
    [SerializeField] GameObject gameCalcScreen;
    [SerializeField] GameObject gameParent;
    public GameObject blackScreen;
    [SerializeField] GameObject[] gameTabs;
    [SerializeField] StudentAdmissionManager studentAdmissionManager;
    [SerializeField] NewspaperManager newspaperManager;
    [SerializeField] Animator mainScreenAnimator;
    [SerializeField] GameObject legendaryStudentPanel;
    [SerializeField] GameObject creditScreen;
    [SerializeField] TextMeshProUGUI dayText;

    public List<Animator> windowAnimators;
    public List<Canvas> windowCanvases;
    private int currentOpenWindow = -1;
    private int sortingLayerBase = 10;
    [SerializeField] private float windowToggleWait = 0.1f;

    public int playerMaxHealth;
    public int playerHealth;

    private bool creditScreenOpen = false;
    public LevelManager currentLevelManager;
    public List<LevelData> levelDataList = new();
    public int currentLevelID = 0;


    

    // Public accessor for the singleton instance
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            LoadCurrentLevelID(); // Load the current level ID
            currentLevelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
            if (currentLevelID < levelDataList.Count)
            {
                Debug.Log("Setting level data as id=" + currentLevelID);
                currentLevelManager.levelData = levelDataList[currentLevelID];
            }
            else
            {
                Debug.LogError("Level ID out of range!");
            }
            
        }
    }

    private void Start()
    {
        mainScreenAnimator = gameTabs[0].GetComponent<Animator>();
        mainScreenAnimator.SetTrigger("LoadIn");
        Debug.Log("now level data id=" + currentLevelID);
        if(currentLevelID > 0 && AdsManager.Instance != null){
            AdsManager.Instance.interstitialAds.Invoke("ShowInterstitialAd", 1f);
        }
    }

    private void Update()
    {
        dayText.text = "Day " + (currentLevelID+1).ToString();
        //Debug.Log("updating level data id=" + currentLevelID);

    }
    public void GameLose()
    {
        SoundManager.Instance.PlaySFX("FireSlam");
        gameLoseScreen.SetActive(true);
    }

    public void GameCalc()
    {
        gameCalcScreen.SetActive(true);
        NewspaperManager.Instance.CheckAllNewsEndings();
        newspaperManager.SelectNews(3);
        gameParent.SetActive(false);
        currentLevelID += 1; // Update level ID
        SaveCurrentLevelID(); // Save the updated level ID
        // ReloadCurrentScene(); // Optional: reload the scene if needed
    }

    public void LoadOut()
    {
        inGame = false;
        mainScreenAnimator.SetTrigger("LoadOut");
    }
    public void WinOrNot()
    {
        if (win)
        {
            GameCalc();
        }
        else
        {
            GameLose();           
        }
    }

    public void ReloadCurrentScene()
    {
        Debug.Log("reloading current scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void SaveCurrentLevelID()
    {
        PlayerPrefs.SetInt("CurrentLevelID", currentLevelID);
        PlayerPrefs.Save();
    }

    public void LoadCurrentLevelID()
    {
        if (PlayerPrefs.HasKey("CurrentLevelID"))
        {
            currentLevelID = PlayerPrefs.GetInt("CurrentLevelID");
        }else{
            currentLevelID = 0;
            PlayerPrefs.Save();
        }
    }

    // Method to clear PlayerPrefs with a context menu option
    [ContextMenu("Clear PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared!");
    }

    public void SwitchToTab(GameObject tab)
    {
        foreach (GameObject go in gameTabs)
        {
            go.SetActive(go == tab);
        }
    }
    // the toggles are not beautiful enough, very easy to make mistake and miss changing variables while copying
    #region Toggles 


    public void ToggleCreditScreen()
    {
        creditScreen.GetComponent<Animator>().SetBool("Expand", creditScreenOpen = !creditScreenOpen);
        SoundManager.Instance.PlaySFX(creditScreenOpen ? "Click_ChatOpen" : "Click_ChatClose");
    }

    public void OnWindowButtonClicked(int windowIndex)
    {
        // If the clicked window is already open, do nothing
        if (currentOpenWindow == windowIndex) 
        {
            windowAnimators[windowIndex].SetBool("Expand", false);
            SoundManager.Instance.PlaySFX("Click_ChatClose");
            currentOpenWindow = windowAnimators.Count + 1;
            return;
        }

        // Start the process of collapsing other windows and opening the new one
        StartCoroutine(CollapseOthersAndOpen(windowIndex));
    }

    // Coroutine to collapse all other windows and open the clicked window
    private IEnumerator CollapseOthersAndOpen(int windowIndex)
    {
        // Collapse all windows that are open (except the clicked one)
        for (int i = 0; i < windowAnimators.Count; i++)
        {
            if (i != windowIndex && windowAnimators[i].GetBool("Expand"))
            {
                windowAnimators[i].SetBool("Expand", false);
                if (windowToggleWait != 0)
                {
                    SoundManager.Instance.PlaySFX("Click_ChatClose");
                }
                yield return new WaitForSeconds(windowToggleWait);
            }
        }

        // Open the clicked window and bring it to the front
        OpenWindow(windowIndex);
    }

    // Method to open the selected window and bring it to the front
    private void OpenWindow(int windowIndex)
    {
        // Open the window
        windowAnimators[windowIndex].SetBool("Expand", true);
        SoundManager.Instance.PlaySFX("Click_ChatOpen");
        UpdateSortingLayer(windowIndex);

        // Update the current open window tracker
        currentOpenWindow = windowIndex;
    }

    private void UpdateSortingLayer(int windowIndex)
    {
        for (int i = 0; i < windowCanvases.Count; i++)
        {
            // The currently selected window gets the highest sorting order
            if (i == windowIndex)
            {
                windowCanvases[i].sortingOrder = sortingLayerBase + windowCanvases.Count;
            }
            else
            {
                // Other windows get lower sorting orders in the stack
                windowCanvases[i].sortingOrder = sortingLayerBase + i;
            }
        }
    }
    #endregion
}
