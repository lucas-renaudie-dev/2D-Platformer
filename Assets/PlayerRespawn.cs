using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    public bool checkpointExists = false;

    private void Awake() {
        playerHealth = GetComponent<Health>();
    }

    public void StartRespawn() {
        if (!checkpointExists) {
            return;
        }
        transform.position = new Vector3 (currentCheckpoint.position.x, -2.07f, 0);
        transform.localScale = new Vector3(-1, 1, 0);
        transform.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        playerHealth.Respawn();

        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent.parent); //change to .parent if checkpoint is direct child of room object (currently it is child of child because it is in the stage folder)
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Checkpoint") {
            currentCheckpoint = other.transform;
            //SoundManager.instance.PlaySound(checkpointSound);
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Animator>().SetTrigger("checkpointPassed");
            checkpointExists = true;
        }
    }
}
