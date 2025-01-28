using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    //Room Camera (not used)
    [SerializeField] private float speed= 0.3f;
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;

    //Follow Player
    [SerializeField] private Transform player;
    public Transform checkpointRoom;


    private void Update() {
        //Room Camera (not used)
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow Player
        if (checkpointRoom.position.x > player.position.x) {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(checkpointRoom.position.x, transform.position.y, transform.position.z), ref velocity, speed);
        }
        else {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

    //Room Camera (not used)
    public void MoveToNewRoom(Transform _newRoom) {
        currentPositionX = _newRoom.position.x;
    }
}
