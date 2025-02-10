using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected Transform nextRoom;
    [SerializeField] protected Transform prevRoom;
    [SerializeField] protected CameraController cam;
    public GameObject doorInner;

    private void Start() {
        nextRoom.GetComponent<Room>().ActivateRoom(false);
    }

    //Camera moves to new room + sets checkpoint (can't go back)
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player" && other.transform.position.x > transform.position.x) {
            Debug.Log("door trigger exit");
            
            cam.MoveToNewRoom(nextRoom);
            doorInner.SetActive(true);

            nextRoom.GetComponent<Room>().ActivateRoom(true);
            prevRoom.GetComponent<Room>().ActivateRoom(false);
        }
    }
}

