using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake() {
        gameOverScreen.SetActive(false);
    }
    
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
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
