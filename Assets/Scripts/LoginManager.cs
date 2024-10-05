using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField loginInputField;
    public TMP_InputField passwordInputField;
    public GameObject blackScreen;
    public Animator animator;
    public string correctUsername = "1";
    public string correctPassword = "1";
    public string nextSceneName = "JiahaoTestScene"; // Name of the next scene

    private void Update()
    {
        // Check if the user pressed Tab to move to the next input field
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Check which input field is currently selected and move to the next one
            if (loginInputField.isFocused)
            {
                passwordInputField.Select();
            }
            else if (passwordInputField.isFocused)
            {
                loginInputField.Select();
            }
        }

        // Check if the user pressed Enter to attempt login
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckCredentials();
        }
    }

    public void CheckCredentials()
    {
        string enteredUsername = loginInputField.text;
        string enteredPassword = passwordInputField.text;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX("Click_Confirm");
        }

        if (enteredUsername == correctUsername && enteredPassword == correctPassword)
        {
            // Credentials match, load the next scene
            blackScreen.SetActive(true);
            animator.SetBool("Correct", true);
        }
        else
        {
            Debug.Log("Entered Username: " + enteredUsername);
            Debug.Log("Entered Password: " + enteredPassword);
            Debug.Log("Correct Username: " + correctUsername);
            Debug.Log("Correct Password: " + correctPassword);
            Debug.Log("Incorrect username or password!");
            // Optionally, display an error message to the player
        }
    }
}
