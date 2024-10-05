using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        SoundManager.Instance.PlaySFX("Click_ChatOpen");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        SoundManager.Instance.PlaySFX("Click_OK");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home(int sceneID)
    {
        SoundManager.Instance.PlaySFX("Click_OK");
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
