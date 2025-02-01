using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private GameObject currentCheckpoint;
    private Health playerHealth;
    public bool checkpointExists = false;

    private void Awake() {
        playerHealth = GetComponent<Health>();
    }

    public void StartRespawn() {
        if (!checkpointExists) {
            return;
        }
        transform.position = new Vector3 (currentCheckpoint.transform.position.x, -2.07f, 0);
        transform.localScale = new Vector3(-1, 1, 0);
        transform.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        playerHealth.Respawn();

        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.transform.parent.parent); //change to .parent if checkpoint is direct child of room object (currently it is child of child because it is in the stage folder)
    
        GameObject[] doorArray = currentCheckpoint.GetComponent<Checkpoint>().doors;
        for (int i = 0; i < doorArray.Length; i++) {
            doorArray[i].GetComponent<Door>().doorInner.SetActive(false);
        }
        GameObject[] signArray = currentCheckpoint.GetComponent<Checkpoint>().signs;
        for (int i = 0; i < signArray.Length; i++) {
            signArray[i].GetComponent<NextRoomSign>().signPassed = false;
            signArray[i].GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Checkpoint") {
            currentCheckpoint = other.gameObject;
            //SoundManager.instance.PlaySound(checkpointSound);
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Animator>().SetTrigger("checkpointPassed");
            checkpointExists = true;
        }
    }
}
