using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected Transform nextRoom;
    [SerializeField] protected Transform prevRoom;
    [SerializeField] protected CameraController cam;
    public GameObject doorInner;

    //Camera moves to new room + sets checkpoint (can't go back)
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player" && other.transform.position.x > transform.position.x) {
            Debug.Log("door trigger exit");
            
            cam.MoveToNewRoom(nextRoom);
            doorInner.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRespawn>().currentRoom = nextRoom;

            nextRoom.GetComponent<Room>().SetActiveRoomTrue();
            prevRoom.GetComponent<Room>().ToggleRoomOffAfterDelay(0.5f);
        }
    }
}

