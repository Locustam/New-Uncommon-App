using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]LoginManager loginManager;
    // Start is called before the first frame update
    public void SceneSwitch()
    {
        SceneManager.LoadScene(loginManager.nextSceneName);
    }
}
