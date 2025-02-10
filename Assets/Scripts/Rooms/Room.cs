using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPositions;

    void Awake()
    {
        initialPositions = new Vector3[enemies.Length];
        for (int i=0; i<enemies.Length; i++) {
            if (enemies[i] != null) {
                initialPositions[i] = enemies[i].transform.position;
            }
        }
    }

    //TODO: chatgpt coroutine 0.5s
    public void ActivateRoom(bool _status) {
        for (int i=0; i<enemies.Length; i++) {
            if (enemies[i] != null) {
                enemies[i].transform.position = initialPositions[i];
                enemies[i].SetActive(_status);
            }
        }
    }
}
