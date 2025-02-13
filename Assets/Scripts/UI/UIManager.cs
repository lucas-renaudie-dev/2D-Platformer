using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip pauseSound;

    //[SerializeField] private AudioClip unpauseSound;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverScreen.activeInHierarchy) {
            if (pauseScreen.activeInHierarchy) {
                SoundManager.instance.PlaySound(pauseSound); //SoundManager.instance.PlaySound(unpauseSound);
                PauseGame(false);
            }
            else {
                SoundManager.instance.PlaySound(pauseSound);
                PauseGame(true);    
            }
        }
    }

    private void Awake() {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    
    #region Game Over
    public void GameOver() {
        SoundManager.instance.StopMusic();
        SoundManager.instance.PlaySound(gameOverSound);
        gameOverScreen.SetActive(true);
    }

    public void Restart() {
        SoundManager.instance.PlayMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void MainMenu() {
        SoundManager.instance.StopMusic();
        SoundManager.instance.PlayMenuMusic();
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
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

        if (status) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    public void SoundVolume() {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume() {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion
}
