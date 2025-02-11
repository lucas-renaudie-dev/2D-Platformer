using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPositions;
    private bool desiredActiveState;

    void Awake()
    {
        initialPositions = new Vector3[enemies.Length];
        for (int i=0; i<enemies.Length; i++) {
            if (enemies[i] != null) {
                initialPositions[i] = enemies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool _status) {
        for (int i=0; i<enemies.Length; i++) {
            if (enemies[i] != null) {
                enemies[i].transform.position = initialPositions[i];
                enemies[i].SetActive(_status);
            }
        }
    }

    public void ToggleRoomOffAfterDelay(float delay) {
        desiredActiveState = false;
        StartCoroutine(DoToggleRoomOff(delay));
    }

    private IEnumerator DoToggleRoomOff(float delay) {
        float timer = 0f;
        while (timer < delay) {
            if (desiredActiveState == true){
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        ActivateRoom(false);
    }

    public void SetActiveRoomTrue() {
        desiredActiveState = true;
        ActivateRoom(true);
    }
}

