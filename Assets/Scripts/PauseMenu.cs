using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;  // Correct attribute
    [SerializeField] private GameObject menuPauseButton;  // Correct attribute

    // Method to pause the game
    public void Pause()
    {
        Time.timeScale = 0f;
        if (pauseButton != null) 
        {
            pauseButton.SetActive(false);
        }
        if (menuPauseButton != null) 
        {
            menuPauseButton.SetActive(true);
        }
    }

    // Method to resume the game
    public void Resume()
    {
        Time.timeScale = 1f;
        if (pauseButton != null) 
        {
            pauseButton.SetActive(true);
        }
        if (menuPauseButton != null) 
        {
            menuPauseButton.SetActive(false);
        }
    }
}
