using Unity.VisualScripting;
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
            cam.MoveToNewRoom(nextRoom);
            doorInner.SetActive(true);

            nextRoom.GetComponent<Room>().ActivateRoom(true);
            prevRoom.GetComponent<Room>().ActivateRoom(false);
        }
    }
}

