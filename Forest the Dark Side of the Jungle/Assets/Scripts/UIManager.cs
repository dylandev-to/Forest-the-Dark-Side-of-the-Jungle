using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject deadScreen;
    [SerializeField]
    private GameObject winScreen;

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
    }
}
