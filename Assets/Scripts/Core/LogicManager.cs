using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    
    //TODO: 
    //lower volume of game over sound

    //fix enemy health = 1 when difficulty impossible

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
