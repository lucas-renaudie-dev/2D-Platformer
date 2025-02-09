using Unity.VisualScripting;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private Collider2D collider;
    public bool signPassed = false;
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private AudioClip victorySound;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !signPassed)
        {
            collider.enabled = false;
            signPassed = true;
            
            SoundManager.instance.StopMusic();
            SoundManager.instance.PlaySound(victorySound);
            levelCompleteScreen.SetActive(true);
        }
    }
}
