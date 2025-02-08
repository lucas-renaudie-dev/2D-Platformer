using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (collision.GetComponent<Health>().currentHealth < 3) {
                SoundManager.instance.PlaySound(pickupSound);
                collision.GetComponent<Health>().AddHealth(1, gameObject);
            }
        }
    }
}
