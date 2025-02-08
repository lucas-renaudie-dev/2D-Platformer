using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    
    //TODO: 
    //"Play" (start game sound)

    //Easy mode: checkpoints, 3 lives
    //Hard mode: no checkpoints, 3 lives
    //Impossible mode: no checkpoints, 1 life

    //"Level complete !" screen with number of coins, Main Menu and Quit buttons

    //Fix room trap reset (checkpoint? doors?)
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
