using UnityEngine;
using UnityEngine.UI;

public class DifficultyScript : MonoBehaviour
{
    public static DifficultyScript Instance;
    
    public string currentDifficulty { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Enforce single instance
            return;
        }

        Instance = this;
        // Make this GameObject persist across scene loads
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        currentDifficulty = "EASY";
    }

    public void ChangeDifficulty()  {
        if (currentDifficulty == "EASY") {
            currentDifficulty = "MEDIUM";
        }
        else if (currentDifficulty == "MEDIUM") {
            currentDifficulty = "HARD";
        }
        else if (currentDifficulty == "HARD") {
            currentDifficulty = "IMPOSSIBLE";
        }
        else {
            currentDifficulty = "EASY";
        }
        
        gameObject.GetComponent<Text>().text = "DIFFICULTY: " + currentDifficulty;
    }
}
