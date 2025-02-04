using System;
using Unity.VisualScripting;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(1, gameObject);
        }
    }
}
