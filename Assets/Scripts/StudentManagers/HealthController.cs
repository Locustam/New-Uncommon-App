using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Array to store the health sign GameObjects
    [SerializeField] private GameObject[] healthSign;

    // Public function to update the health signs based on player health and max health
    public void UpdateHealthSign(int playerHealth, int maxHealth)
    {
        // Ensure the input health values are valid
        if (playerHealth < 0 || playerHealth > maxHealth || maxHealth > healthSign.Length+1)
        {
            Debug.LogError("Invalid playerHealth or maxHealth values.");
            return;
        }

        // Loop through the healthSign array and toggle the appropriate signs
        for (int i = 0; i < healthSign.Length; i++)
        {
            // Turn on the health sign if it matches the player's health
            if (i == maxHealth - playerHealth -1)
            {
                healthSign[i].SetActive(true);  // Turn on the correct health sign
            }
            else
            {
                healthSign[i].SetActive(false); // Turn off all other health signs
            }
        }
    }
}
