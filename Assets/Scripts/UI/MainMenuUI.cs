using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private DifficultyScript script;

    public void Difficulty() {
        script.ChangeDifficulty();
    }
    
    public void Play() {
        SoundManager.instance.StopMenuMusic();
        SoundManager.instance.PlayMusic();
        SceneManager.LoadSceneAsync(1);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void Quit() {
        Application.Quit(); //only works when built the game

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
