using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCollector : MonoBehaviour
{
    public int totalGems = 5; // Total number of gems required to win
    public Text gemCounterText; // UI Text to display gem count
    public GameObject winUI; // UI panel to show when the player wins

    private int gemsCollected = 0;
    private bool hasWon = false;

    void Start()
    {
        if (gemCounterText != null)
            gemCounterText.text = $"Gems: {gemsCollected}/{totalGems}";

        if (winUI != null)
            winUI.SetActive(false); // Hide the Win UI initially
    }

    public void CollectGem()
    {
        if (hasWon) return; // Stop counting if the player already won

        gemsCollected++;
        if (gemCounterText != null)
            gemCounterText.text = $"Gems: {gemsCollected}/{totalGems}";

        if (gemsCollected >= totalGems)
        {
            TriggerWinCondition();
        }
    }

    void TriggerWinCondition()
    {
        hasWon = true; // Mark the win state
        Debug.Log("You collected all the gems! You win!");

        if (winUI != null)
            winUI.SetActive(true); // Show the Win UI
    }
}
