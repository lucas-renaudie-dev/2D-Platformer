using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private CoinScript coinScript;
    private GameObject currentCheckpoint;
    private Health playerHealth;
    public bool checkpointExists = false;
    public Transform currentRoom;

    private void Awake() {
        playerHealth = GetComponent<Health>();
    }

    public void StartRespawn() {
        //deactivate room where the player died
        currentRoom.GetComponent<Room>().ToggleRoomOffAfterDelay(0.5f);

        transform.position = new Vector3 (currentCheckpoint.transform.position.x, -2.07f, 0);
        transform.localScale = new Vector3(-1, 1, 0);
        transform.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        playerHealth.Respawn();

        coinScript.currentCoins = currentCheckpoint.GetComponent<Checkpoint>().coins;
        coinScript.AddCoin(0);
        currentCheckpoint.GetComponent<Checkpoint>().ResetCoins();

        currentCheckpoint.GetComponent<Checkpoint>().ResetTraps();

        Transform checkpointRoom = currentCheckpoint.transform.parent.parent; 
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(checkpointRoom); //change to .parent if checkpoint is direct child of room object (currently it is child of child because it is in the stage folder)
        checkpointRoom.GetComponent<Room>().SetActiveRoomTrue();
        currentRoom = checkpointRoom; //current room becomes the checkpoint room

        currentCheckpoint.GetComponent<Checkpoint>().ResetDoors();
        currentCheckpoint.GetComponent<Checkpoint>().ResetSigns();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Checkpoint" && playerHealth.currentHealth > 0) {
            currentCheckpoint = other.gameObject;
            currentCheckpoint.GetComponent<Checkpoint>().coins = coinScript.currentCoins;
            SoundManager.instance.PlaySound(checkpointSound);
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Animator>().SetTrigger("checkpointPassed");
            checkpointExists = true;
        }
    }
}
