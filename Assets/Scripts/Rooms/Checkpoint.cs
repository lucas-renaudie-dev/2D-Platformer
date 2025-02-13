using System.Collections;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] signs;
    public GameObject[] enemiesInCheckpointRoom; //including those in checkpoint room
    public GameObject[] enemiesAfterCheckpointRoom; //including those in checkpoint room

    private void Awake() {
        string diff = DifficultyScript.Instance.currentDifficulty;
        
        if (diff == "EASY")
            gameObject.SetActive(true);
        else 
            gameObject.SetActive(false);
    }

    //--------------------------------------------- TRAP RESET ---------------------------------------------------------------------------------------------------------------------
    public void ResetTraps() {
        ResetTrapsInCheckpointRoom();
        ResetTrapsAfterCheckpointRoom();
    }

    void ResetTrapsInCheckpointRoom() {
        resetTraps(enemiesInCheckpointRoom);
    }

    void ResetTrapsAfterCheckpointRoom() {
        StartCoroutine(ResetTrapsAfterDelay(0.6f)); //reset all traps after the current checkpoint room (after at least the delay required to toggle the rooms off, which is 0.5, so let's do 0.6)
    }

    private IEnumerator ResetTrapsAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        resetTraps(enemiesAfterCheckpointRoom);
    }

    void resetTraps(GameObject[] traps) {
        foreach (var trap in traps)
        {
            if (trap.GetComponent<EnemyPatroll>() != null) {
                trap.GetComponent<EnemyPatroll>().ResetTrap();
            }
            if (trap.GetComponent<EnemySideways>() != null) {
                trap.GetComponent<EnemySideways>().ResetTrap();
            }
            if (trap.GetComponent<SpikeHead>() != null) {
                trap.GetComponent<SpikeHead>().ResetTrap();
            }
            if (trap.GetComponent<RangedEnemy>() != null) {
                trap.GetComponent<RangedEnemy>().ResetTrap();
            }
            if (trap.GetComponent<ArrowTrap>() != null) {
                trap.GetComponent<ArrowTrap>().ResetTrap();
            }
            if (trap.GetComponent<Health>() != null) {
                trap.GetComponent<Health>().ActivateComponents();
            }
        }
    }
}
