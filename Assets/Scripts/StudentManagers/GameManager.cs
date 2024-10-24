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
    [SerializeField] Animator appScreenAnimator, chatScreenAnimator, mapScreenAnimator,mainScreenAnimator;
    [SerializeField] GameObject legendaryStudentPanel;
    [SerializeField] GameObject creditScreen;
    [SerializeField] TextMeshProUGUI dayText;
    GameObject screenOnTop;
    public int playerMaxHealth;
    public int playerHealth;

    private bool appScreenOpen = true, chatScreenOpen = false, mapScreenOpen = false, legendaryStudentPanelOpen = false, creditScreenOpen = false;
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

    public void ToggleAppScreen()
    {
        screenOnTop = appScreenAnimator.gameObject;
        if (!appScreenOpen)
        {
            appScreenAnimator.SetBool("Expand", appScreenOpen = !appScreenOpen);
            BringWindowToFront(screenOnTop);
        }
        else
        {
            if (IsOnTop())
            {
                appScreenAnimator.SetBool("Expand", appScreenOpen = !appScreenOpen);
                Transform parent = appScreenAnimator.gameObject.transform.parent;
                int currentIndex = appScreenAnimator.gameObject.transform.GetSiblingIndex();

                if (currentIndex > 0)
                {
                    // Get the window directly below (next sibling)
                    GameObject nextWindow = parent.GetChild(currentIndex - 1).gameObject;
                    BringWindowToFront(nextWindow); // Bring the next window to the front
                }
            }
            else
            {
                BringWindowToFront(screenOnTop);
            }
        }
        SoundManager.Instance.PlaySFX(appScreenOpen ? "Click_ChatOpen" : "Click_ChatClose");
    }

    public void ToggleChatScreen()
    {
        screenOnTop = chatScreenAnimator.gameObject;
        if (!chatScreenOpen)
        {
            chatScreenAnimator.SetBool("Expand", chatScreenOpen = !chatScreenOpen);
            BringWindowToFront(screenOnTop);
        }
        else
        {
            if (IsOnTop())
            {
                chatScreenAnimator.SetBool("Expand", chatScreenOpen = !chatScreenOpen);
                Transform parent = chatScreenAnimator.gameObject.transform.parent;
                int currentIndex = chatScreenAnimator.gameObject.transform.GetSiblingIndex();

                if (currentIndex > 0)
                {
                    // Get the window directly below (next sibling)
                    GameObject nextWindow = parent.GetChild(currentIndex - 1).gameObject;
                    BringWindowToFront(nextWindow); // Bring the next window to the front
                }
            }
            else
            {
                BringWindowToFront(screenOnTop);
            }
        }
        SoundManager.Instance.PlaySFX(chatScreenOpen ? "Click_ChatOpen" : "Click_ChatClose");
    }

    public void ToggleMapScreen()
    {

         screenOnTop = mapScreenAnimator.gameObject;
        if(!mapScreenOpen)
        {
            mapScreenAnimator.SetBool("Expand", mapScreenOpen = !mapScreenOpen);
            BringWindowToFront(screenOnTop);
        }
        else
        {
            if (IsOnTop())
            {
                mapScreenAnimator.SetBool("Expand", mapScreenOpen = !mapScreenOpen);
                Transform parent = mapScreenAnimator.gameObject.transform.parent;
                int currentIndex = mapScreenAnimator.gameObject.transform.GetSiblingIndex();

                if (currentIndex > 0)
                {
                    // Get the window directly below (next sibling)
                    GameObject nextWindow = parent.GetChild(currentIndex - 1).gameObject;
                    BringWindowToFront(nextWindow); // Bring the next window to the front
                }
            }
            else
            {
                BringWindowToFront(screenOnTop);
            }
        }
        SoundManager.Instance.PlaySFX(mapScreenOpen ? "Click_ChatOpen" : "Click_ChatClose");
    }


    public void ToggleLegendaryStudentPanel() //unconventional naming & referencsing scheme
    {
        screenOnTop = legendaryStudentPanel;
        if (!legendaryStudentPanelOpen)
        {
            legendaryStudentPanel.GetComponent<Animator>().SetBool("Expand", legendaryStudentPanelOpen = !legendaryStudentPanelOpen);
            BringWindowToFront(screenOnTop);
        }
        else
        {
            if (IsOnTop())
            {
                legendaryStudentPanel.GetComponent<Animator>().SetBool("Expand", legendaryStudentPanelOpen = !legendaryStudentPanelOpen);
                Transform parent = legendaryStudentPanel.transform.parent;
                int currentIndex = legendaryStudentPanel.transform.GetSiblingIndex();

                if (currentIndex > 0)
                {
                    // Get the window directly below (next sibling)
                    GameObject nextWindow = parent.GetChild(currentIndex - 1).gameObject;
                    BringWindowToFront(nextWindow); // Bring the next window to the front
                }
            }
            else
            {
                BringWindowToFront(screenOnTop);
            }
        }
        SoundManager.Instance.PlaySFX(legendaryStudentPanelOpen ? "Click_ChatOpen" : "Click_ChatClose");
    }

    public void ToggleCreditScreen()
    {
        creditScreen.GetComponent<Animator>().SetBool("Expand", creditScreenOpen = !creditScreenOpen);
        SoundManager.Instance.PlaySFX(creditScreenOpen ? "Click_ChatOpen" : "Click_ChatClose");
    }

    public void BringWindowToFront(GameObject window)
    {
        // Move the window to the last child in the parent (top of the hierarchy)
        window.transform.SetAsLastSibling();
    }

    private bool IsOnTop()
    {
        return screenOnTop.transform.GetSiblingIndex() == screenOnTop.transform.parent.childCount - 1;
    }

    #endregion
}
