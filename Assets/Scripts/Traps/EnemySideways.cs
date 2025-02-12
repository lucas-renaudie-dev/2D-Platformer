using System;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private bool isSaw;
    [SerializeField] private int direction;
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float speed = 3;
    private float timer = 0;
    private float ghostScale = -1;

    // Update is called once per frame
    void Update()
    {
        if (isSaw) {
            IsSaw();
        }
        else {
            IsGhost();
        }
    }

    private void IsSaw() {
        if (timer < maxTime/2) {
            timer += Time.deltaTime;
            switch (direction) {
                case 0:
                    transform.position = transform.position + (new Vector3 (-1, -1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 1:
                    transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;
                    break;
                case 2:
                    transform.position = transform.position + (new Vector3 (-1, 1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 3:
                    transform.position = transform.position + (Vector3.up * speed) * Time.deltaTime;
                    break;
                case 4:
                    transform.position = transform.position + (new Vector3 (1, 1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 5:
                    transform.position = transform.position + (Vector3.right * speed) * Time.deltaTime;
                    break;
                case 6:
                    transform.position = transform.position + (new Vector3 (1, -1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 7:
                    transform.position = transform.position + (Vector3.down * speed) * Time.deltaTime;
                    break;
            }
        }
        else if (timer < maxTime) {
            timer += Time.deltaTime;
            switch (direction) {
                case 0:
                    transform.position = transform.position + (new Vector3 (1, 1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 1:
                    transform.position = transform.position + (Vector3.right * speed) * Time.deltaTime;
                    break;
                case 2:
                    transform.position = transform.position + (new Vector3 (1, -1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 3:
                    transform.position = transform.position + (Vector3.down * speed) * Time.deltaTime;
                    break;
                case 4:
                    transform.position = transform.position + (new Vector3 (-1, -1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 5:
                    transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;
                    break;
                case 6:
                    transform.position = transform.position + (new Vector3 (-1, 1, 0) * speed/(float)Math.Sqrt(2)) * Time.deltaTime;
                    break;
                case 7:
                    transform.position = transform.position + (Vector3.up * speed) * Time.deltaTime;
                    break;
            }
        }
        else {
            timer = 0;
        }
    }

    private void IsGhost() {
        if (timer < maxTime) {
            timer += Time.deltaTime;
        }
        else {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            timer = 0;
            ghostScale = -ghostScale;
        }

        if (ghostScale == -1) {
            transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;
        }
        else {
            transform.position = transform.position + (Vector3.right * speed) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void ResetTrap() {
        timer = 0;
        ghostScale = -1;
    }
}
