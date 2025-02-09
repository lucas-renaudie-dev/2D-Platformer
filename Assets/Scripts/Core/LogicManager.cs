using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    
    //TODO: 
    //Chill music for main menu and game music when start/restart game
    //lower volume of game over sound
    //"Level complete !" screen with number of coins, Main Menu and Quit buttons

    //Main Menu screen with coins, hearts, fireballs, arrows, enemies, spike traps, etc. 
    //player idle next to title
    //better background image

    //Fix room trap reset (checkpoint? doors?)

    //Create game!
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
