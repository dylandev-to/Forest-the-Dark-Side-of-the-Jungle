using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    // Public variables for easy setup in the Unity Inspector
    public float startTime = 60f; // Starting time in seconds
    public Text timerText; // Assign a UI Text component in the Inspector
    public GameObject gameOverUI; // UI panel to show when time runs out

    private float currentTime;
    private bool isGameOver = false;

    void Start()
    {
        currentTime = startTime; // Initialize the timer
        if (gameOverUI != null)
            gameOverUI.SetActive(false); // Hide the Game Over UI initially
    }

    void Update()
    {
        if (isGameOver) return; // Stop updating if the game is over

        // Decrease the timer
        currentTime -= Time.deltaTime;

        // Update the timer display
        if (timerText != null)
            timerText.text = Mathf.Max(0, currentTime).ToString("F0"); // Show seconds as whole numbers

        // Check if the timer reaches zero
        if (currentTime <= 0)
        {
            TriggerLoseCondition();
        }
    }

    void TriggerLoseCondition()
    {
        isGameOver = true; // Prevent further updates
        Debug.Log("Time's up! You lose!");

        if (gameOverUI != null)
            gameOverUI.SetActive(true); // Display Game Over UI
    }
}
