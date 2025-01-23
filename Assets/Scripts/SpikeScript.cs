using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public LogicManager logic;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

     private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            logic.gameOver();
            player.GetComponent<PlayerMovement>().isAlive = false;
        }
    }
}
