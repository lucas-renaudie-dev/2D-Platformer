using Unity.VisualScripting;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            SoundManager.instance.PlaySound(pickupSound);
            gameObject.GetComponentInParent<CoinScript>().AddCoin();
            gameObject.SetActive(false);
        }
    }
}
