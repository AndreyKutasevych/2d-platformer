using UnityEngine.SceneManagement;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Main Menu")] 
    [SerializeField] private GameObject mainMenuScreen;
    

    #region gameOver

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.Instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PauseGame(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion

    #region Pause Game
    private void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        Time.timeScale = status ? 0 : 1;
    }

    public void Resume()
    {
        PauseGame(false);
    }

    public void SoundVolume()
    {
        SoundManager.Instance.ChangeVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }
}
