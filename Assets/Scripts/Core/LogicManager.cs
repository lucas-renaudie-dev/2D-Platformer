using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    
    //TODO: 
    //Main Menu screen with coins, hearts, fireballs, arrows, enemies, spike traps, etc. 
    //player idle next to title
    //better background image

    //Fix room enemies and trap reset (checkpoint? doors?)
    //Also deactivate all enemies and traps after death

    //Create game!
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
