using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    
    //TODO: 
    //Main Menu screen with coins, hearts, fireballs, arrows, enemies, spike traps, etc. 
    //player idle next to title
    //better background image

    //Fix room enemies and trap reset (checkpoint? doors?)
    //Also deactivate collidng layers 9 and 10 after death? 
    //or go in every enemy script and add an if statement in the ontriggerenter2D

    //Create game!
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
