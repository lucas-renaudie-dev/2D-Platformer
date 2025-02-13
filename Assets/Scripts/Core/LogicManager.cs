using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    
    //TODO: 
    //Main Menu screen with coins, hearts, fireballs, arrows, enemies, spike traps, etc. 
    //player idle next to title
    //better background image

    //Create game!
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
