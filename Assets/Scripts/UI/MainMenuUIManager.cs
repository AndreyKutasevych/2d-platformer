using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu")] 
    [SerializeField] private GameObject mainMenuScreen;
    

    #region gameOver

    private void Awake()
    {
        
    }
    
    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }



    public void Resume()
        {
            
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

        public void StartNewGame()
        {
            SceneManager.LoadScene(1);
        }
}
