using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private DifficultyScript script;

    public void Difficulty() {
        script.ChangeDifficulty();
    }
    
    public void Play() {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit() {
        Application.Quit(); //only works when built the game

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
