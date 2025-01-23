using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public LogicManager logic;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            logic.gameOver();
            player.GetComponent<PlayerMovement>().isAlive = false;
        }
    }
}
