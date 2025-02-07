using UnityEngine;
using UnityEngine.UI;

public class DifficultyScript : MonoBehaviour
{
    private string currentDifficulty;

    private void Awake() {
        currentDifficulty = "EASY";
    }

    public void ChangeDifficulty()  {
        if (currentDifficulty == "EASY") {
            currentDifficulty = "MEDIUM";
        }
        else if (currentDifficulty == "MEDIUM") {
            currentDifficulty = "HARD";
        }
        else {
            currentDifficulty = "EASY";
        }
        
        gameObject.GetComponent<Text>().text = "DIFFICULTY: " + currentDifficulty;
    }
}
