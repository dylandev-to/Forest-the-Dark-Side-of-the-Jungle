using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GameObject startMenu;
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
    private TMP_Text scoreText;
    private int score;
    [SerializeField]
    private TMP_Text goldText;
    private int gold;
    [SerializeField]
    public Slider staminaSlider;
    public static Action<int> OnStaminaSliderUpdate;
    private int stamina;
    private bool staminadecrease = false;
    [SerializeField]
    private TMP_Text ammoText;
    private int ammo;
    private bool ammodecrease = false;
    [SerializeField]
    private TMP_Text scaredText;
    private int scaredLevel;
    [SerializeField]
    private float scaredMultiplayer;
    private bool scaredDecrease = false;
    [SerializeField]
    private Slider batterySlider;
    public static Action<int> OnBatteryUpdate;
    [SerializeField]
    public Slider healthSlider;
    public static Action<int> OnHealthSliderUpdate;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        Coin.Collected += UpdateScore;

        OnShowDeadScreen += ShowDeadScreen;
        OnShowWinScreen += ShowWinScreen;

        OnHealthSliderUpdate += delegate (int value)
        {
            healthSlider.value = value;
        };

        OnStaminaSliderUpdate += delegate (int value)
        {
            staminaSlider.value = value;
        };

        OnBatteryUpdate += delegate (int value)
        {
            batterySlider.value = value;
        };

        Enemy.OnEnemyKill += () => score += 10;
        Enemy.OnHitEnemy += () => score += 3;
    }

    // Update is called once per frame
    void Update()
    {
        StopDeadScreen();
        UpdateScaredLevel();

        try
        {
            scoreText.text = $"Score: {score.ToString().PadLeft(4, '0')}";
        }
        catch { }
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

        scaredLevel = 0;
    }

    private float _cooldownScared;
    private void UpdateScaredLevel()
    {
        _cooldownScared += Time.deltaTime * scaredMultiplayer;
        if (_cooldownScared > 1)
        {
            scaredLevel++;
            _cooldownScared = 0;

            try
            {
                scaredText.text = $"Scared Level: {scaredLevel.ToString().PadLeft(4, '0')}";
            }
            catch { }
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
        try
        {
            score += 3;
            gold++;
        }
        catch { }
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
