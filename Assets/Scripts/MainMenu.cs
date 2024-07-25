using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Make sure this is included to work with UI elements

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject settingsUI;
    public GameObject gameM;
    public Button Quit_button; // Ensure this is assigned in the Inspector

    void Start()
    {
        gameM = GameObject.Find("GameManager");
        ShowMainMenu();

        // Add listener to the Quit button
        if (Quit_button != null)
        {
            Quit_button.onClick.AddListener(QuitGame);
        }
      
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void StartGame()
    {
        Destroy(gameM);
        SceneManager.LoadScene("01_TrainingScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        // If running in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running as a standalone application
        Application.Quit();
        #endif
    }
}
