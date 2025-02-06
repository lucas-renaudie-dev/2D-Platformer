using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseScreen.activeInHierarchy) {
                PauseGame(false);
            }
            else {
                PauseGame(true);    
            }
        }
    }

    private void Awake() {
        gameOverScreen.SetActive(false);
    }
    
    #region Game Over
    public void GameOver() {
        SoundManager.instance.PlaySound(gameOverSound);
        gameOverScreen.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit(); //only works when built the game

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion

    #region Pause

    public void PauseGame(bool status) {
        pauseScreen.SetActive(status);
    }

    public void SoundVolume() {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume() {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion
}
