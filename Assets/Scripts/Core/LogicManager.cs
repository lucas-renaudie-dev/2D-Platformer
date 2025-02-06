using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    
    //TODO: 

    //Pause and Game Over both stop the scene

    //Main Menu screen with:
    //"Difficulty: Easy Mode" (smaller) (arrow sound)
    //"Play" (big) (start game sound)
    //Further down a Quit button (smaller) (arrow sound)

    //Easy mode: checkpoints, 3 lives
    //Hard mode: no checkpoints, 3 lives
    //Impossible mode: no checkpoints, 1 life

    //"Level complete !" screen with number of coins, Main Menu and Quit buttons

    //Coins UI and logic
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
