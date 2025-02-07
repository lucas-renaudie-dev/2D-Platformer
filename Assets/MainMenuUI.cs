using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    [Header ("Main Menu")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private DifficultyScript script;

    //[SerializeField] private AudioClip unpauseSound;

    private void Awake() {
        mainMenuScreen.SetActive(true);
    }

    private void Start() {
        mainMenuScreen.SetActive(true);
    }

    public void Difficulty() {
        script.ChangeDifficulty();
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }
    
    public void Quit() {
        Application.Quit(); //only works when built the game

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
