using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainMenuPanel; // Your start/exit buttons panel
    public GameObject pauseMenuPanel; // Your resume/quit panel

    private bool isPlaying = false;

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        // Only check for pause if we're in gameplay
        if (isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        isPlaying = true;
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Unpause game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void TogglePause()
    {
        bool shouldPause = !pauseMenuPanel.activeSelf;
        pauseMenuPanel.SetActive(shouldPause);
        Time.timeScale = shouldPause ? 0f : 1f;
        Cursor.lockState = shouldPause ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = shouldPause;
    }

    public void ResumeGame() => TogglePause();

    public void QuitToMenu()
    {
        isPlaying = false;
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}