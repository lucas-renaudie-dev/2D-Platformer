using Unity.VisualScripting;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private Collider2D collider;
    public bool signPassed { get; private set; }
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private AudioClip victorySound;

    void Start()
    {
        signPassed = false;
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<Health>().currentHealth > 0 && !signPassed)
        {
            collider.enabled = false;
            signPassed = true;
            
            SoundManager.instance.StopMusic();
            SoundManager.instance.PlaySound(victorySound);

            other.GetComponent<Health>().DeactivateComponents();
            other.GetComponent<Animator>().SetBool("run", false);
            Physics2D.IgnoreLayerCollision(9, 10, true);
            levelCompleteScreen.SetActive(true);
        }
    }
}
