using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    public Slider healthSlider;
    public static Action<int> OnHealthSliderUpdate;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject deadScreen;
    public static Action<bool> OnShowDeadScreen;
    [SerializeField]
    private GameObject winScreen;
    public static Action<bool> OnShowWinScreen;
    [SerializeField]
    private GameObject settingsScreen;

    [Header("Texts")]
    [SerializeField]
    private Text scoreText;
    private int score;
    [SerializeField]
    private Text goldText;
    private int gold;
    [SerializeField]
    private Text staminaText;
    private int stamina;
    private bool staminadecrease = false;
    [SerializeField]
    private Text ammoText;
    private int ammo;
    private bool ammodecrease = false;
    [SerializeField]
    private Text scaredText;
    private int scaredLevel;
    private bool scaredDecrease = false;
    [SerializeField]
    private Text BatteryImage;
    private int flashlightBattery;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        Coin.Collected += UpdateScore;

        OnShowDeadScreen += ShowDeadScreen;
        OnShowWinScreen += ShowWinScreen;

        OnHealthSliderUpdate += delegate (int value)
        {
            Debug.Log(value.ToString());
            healthSlider.value = value;
        };

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        StopDeadScreen();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.StartsWith("Background"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            startMenu.SetActive(false);
            gameUI.SetActive(true);
        }
        else
        {
            startMenu.SetActive(true);
            gameUI.SetActive(false);
        }

        ShowDeadScreen(false);
        ShowWinScreen(false);
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

    void StopDeadScreen()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Background")
        {
            deadScreen.SetActive(false);
        }
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
        goldText.text = $"Gold: {gold.ToString().PadLeft(4, '0')}";
    }

    public void UpdateStamina()
    {
        if (staminadecrease == true)
        {
            stamina -= 2;
            staminaText.text = $"Stamina: {stamina.ToString().PadLeft(4, '0')}";
        }
        else if (staminadecrease == false && stamina < 100)
        {
            stamina += 2;
            staminaText.text = $"Stamina: {stamina.ToString().PadLeft(4, '0')}";
        }
    }

    public void UpdateAmmo()
    {
        if (ammodecrease == true)
        {
            ammo -= 1;
            ammoText.text = $"Ammo: {ammo.ToString().PadLeft(4, '0')}";
        }
        else if (ammodecrease == false && ammo < 100)
        {
            ammo += 5;
            ammoText.text = $"Ammo: {ammo.ToString().PadLeft(4, '0')}";
        }
    }

    public void UpdateScared()
    {
        if (scaredDecrease == true)
        {
            scaredLevel -= 5;
            scaredText.text = $"Scared Level: {scaredLevel.ToString().PadLeft(4, '0')}";
        }
        else if (scaredDecrease == false && scaredLevel < 100)
        {
            scaredLevel += 5;
            scaredText.text = $"Scared Level: {scaredLevel.ToString().PadLeft(4, '0')}";
        }
    }

}
