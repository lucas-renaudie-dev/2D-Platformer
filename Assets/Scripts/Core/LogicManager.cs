using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void gameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //TODO: add game over screen
        //restart from checkpoint
        //restart from beginning
        //Quit (-> home screen with all the levels)

        //Easy mode: checkpoints, 3 lives
        //Hard mode: no checkpoints, 3 lives
        //Impossible mode: no checkpoints, 1 life

        //"Level complete !" screen with next level button and quit button
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
