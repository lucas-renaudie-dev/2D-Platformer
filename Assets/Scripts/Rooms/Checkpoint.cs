using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] signs;
    public GameObject[] enemiesAfterCheckpoint; //including those in checkpoint room

    private void Awake() {
        string diff = DifficultyScript.Instance.currentDifficulty;
        
        if (diff == "EASY")
            gameObject.SetActive(true);
        else 
            gameObject.SetActive(false);
    }
}
