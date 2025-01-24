using JetBrains.Annotations;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public LogicManager logic;
    public GameObject player;
    private float timer = 0;
    public float maxTime = 1.5f;
    private float ghostDirection = -1;
    public float ghostSpeed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < maxTime) {
            timer += Time.deltaTime;
        }
        else {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            timer = 0;
            ghostDirection = -ghostDirection;
        }

        if (ghostDirection == -1) {
            transform.position = transform.position + (Vector3.left * ghostSpeed) * Time.deltaTime;
        }
        else {
            transform.position = transform.position + (Vector3.right * ghostSpeed) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            logic.gameOver();
            player.GetComponent<PlayerMovement>().isAlive = false;
        }
    }
}
