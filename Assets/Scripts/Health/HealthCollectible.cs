using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    
    private void Awake() {
        string diff = DifficultyScript.Instance.currentDifficulty;
        
        if (diff == "EASY" || diff == "MEDIUM")
            gameObject.SetActive(true);
        else 
            gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (collision.GetComponent<Health>().currentHealth < 3) {
                SoundManager.instance.PlaySound(pickupSound);
                collision.GetComponent<Health>().AddHealth(1, gameObject);
            }
        }
    }
}
