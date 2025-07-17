using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // UI variables
    public GameObject _pauseMenuPanel;
    public Button _continueButton;
    public Button _exit;

    private bool isPaused = false;

    void Start()
    {
        // Activates menu items
        _pauseMenuPanel.SetActive(false);
        _continueButton.onClick.AddListener(ResumeGame);
        _exit.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        // Pauses game on escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        _pauseMenuPanel.SetActive(true);
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        _pauseMenuPanel.SetActive(false);
        isPaused = false;
    }

    void ExitGame()
    {
        Debug.Log("Exiting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // Exit in build
#endif
    }
}
