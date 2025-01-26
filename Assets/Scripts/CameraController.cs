using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    //Room Camera
    [SerializeField] private float speed;
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;

    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    [SerializeField] private Transform firstRoom;


    private void Update() {
        //Room Camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow Player
        transform.position = new Vector3(Math.Max(player.position.x + lookAhead, firstRoom.position.x), transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance * (-player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom) {
        currentPositionX = _newRoom.position.x;
    }
}
