using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    public Button quitButton; // Reference to the Quit Button

    void Start()
    {
        if (quitButton != null)
        {
            // Add listener to the button to call QuitGame method when clicked
            quitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("Quit Button not assigned in the Inspector.");
        }
    }

    void QuitGame()
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
