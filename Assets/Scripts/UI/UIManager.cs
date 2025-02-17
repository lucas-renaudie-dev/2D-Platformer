using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip pauseSound;
    
    [Header ("Instructions")]
    [SerializeField] private GameObject instructionScreen;
    [SerializeField] private float totalFlashTime = 3f;    // total duration of flashing
    [SerializeField] private float singleFadeDuration = 0.5f; // time to go from fully visible to invisible or vice versa

    private CanvasGroup canvasGroup;

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

    private void Start() {
        canvasGroup = instructionScreen.GetComponent<CanvasGroup>();
        StartCoroutine(FlashFade());
    }

    
    private IEnumerator FlashFade() {
        // Make sure we're active and fully visible at the start
        instructionScreen.SetActive(true);
        canvasGroup.alpha = 1f;

        float elapsed = 0f;
        bool fadingOut = true;  // Start by fading from alpha=1 to alpha=0

        // We'll keep fading in/out until we've used up totalFlashTime
        while (elapsed < totalFlashTime)
        {
            // Lerp alpha over singleFadeDuration
            float startAlpha = canvasGroup.alpha;
            float endAlpha = fadingOut ? 0f : 1f;
            float fadeTimer = 0f;

            while (fadeTimer < singleFadeDuration)
            {
                fadeTimer += Time.deltaTime;
                // Progress from 0 to 1 over the fade interval
                float t = fadeTimer / singleFadeDuration;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                yield return null;
            }

            // Snap to final alpha
            canvasGroup.alpha = endAlpha;

            // Swap fade direction for next cycle
            fadingOut = !fadingOut;

            // Add one fade duration to elapsed
            elapsed += singleFadeDuration;
        }

        // After we’ve done flashing for totalFlashTime, ensure it's off
        canvasGroup.alpha = 0f;
        instructionScreen.SetActive(false);
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
