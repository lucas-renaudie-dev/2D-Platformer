using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private GameObject doorInner;

    //Room Camera (not used)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (other.transform.position.x < transform.position.x) {
                cam.MoveToNewRoom(nextRoom);
            }
            else {
                cam.MoveToNewRoom(previousRoom);
            }
        }
    }

    //Follow Player + Checkpoint (can't go back)
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player" && other.transform.position.x > transform.position.x) {
            cam.checkpointRoom = nextRoom;
            doorInner.SetActive(true);
        }
    }
}
