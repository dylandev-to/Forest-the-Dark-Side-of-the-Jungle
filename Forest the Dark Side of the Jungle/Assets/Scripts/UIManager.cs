using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject deadScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject settingsScreen;

    [Header("Texts")]
    [SerializeField]
    private Text scoreText;
    private int score;
    [SerializeField]
    private Text goldText;
    private int gold;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;

        Coin.Collected += UpdateScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Background"))
        {
            startMenu.SetActive(true);
        }
        else
        {
            startMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void GoLevel(string levelName)
    {
        try
        {
            SceneManager.LoadScene(levelName);
        winScreen.SetActive(false);
        }
        catch
        {
            Debug.LogError("Level not found");
        }
        deadScreen.SetActive(false);
    }

    public void ShowDeadScreen(bool show)
    {
        deadScreen.SetActive(show);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowWinScreen(bool show)
    {
        winScreen.SetActive(show);
        Cursor.lockState = CursorLockMode.None;
        if (!show)
        {
            winScreen.SetActive(false);
        }
    }

    public void OpenSettings(bool open)
    {
        settingsScreen.SetActive(open);
    }

    public void UpdateScore()
    {
        score += 3;
        scoreText.text = $"Score: {score.ToString().PadLeft(4, '0')}";

        gold++;
        goldText.text = $"Score: {gold.ToString().PadLeft(4, '0')}";
    public void OpenSettings(bool open)
    {
        settingsScreen.SetActive(open);
    }

    public void UpdateScore()
    {
        score += 3;
        scoreText.text = $"Score: {score.ToString().PadLeft(4, '0')}";

        gold++;
        goldText.text = $"Gold: {gold.ToString().PadLeft(4, '0')}";
    }
}
