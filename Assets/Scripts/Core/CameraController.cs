using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int cameraType; //0 = Room, 1 = Room + Player

    [SerializeField] private float speed= 0.3f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    private Transform firstRoom; //Use this (serialize it) when using follow player (not used currently)
    [SerializeField] private Transform checkpointRoom; //in unity, set this to the first room

    private void Update() {
        if (cameraType == 0) {
            FollowRoom();
        }
        if (cameraType == 1) {
            FollowRoomAndPlayer();
        }
        /* Not used currently
        if (cameraType == 2) {
            FollowRoomAndPlayer();
        }
        */
    }

    private void FollowRoom() {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(checkpointRoom.position.x, checkpointRoom.position.y, transform.position.z), ref velocity, speed);
    }
    private void FollowPlayer() { //Not used currently
        if (firstRoom.position.x > player.position.x) {
            transform.position =  new Vector3(firstRoom.position.x, transform.position.y, transform.position.z);
        }
        else {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }
    private void FollowRoomAndPlayer() {
        if (checkpointRoom.position.x > player.position.x) {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(checkpointRoom.position.x, transform.position.y, transform.position.z), ref velocity, speed);
        }
        else {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

    public void MoveToNewRoom(Transform _nextRoom) {
        checkpointRoom = _nextRoom;
    }
}
