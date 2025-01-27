using UnityEngine;

public class SpikeScript : MonoBehaviour
{
     private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }
}
