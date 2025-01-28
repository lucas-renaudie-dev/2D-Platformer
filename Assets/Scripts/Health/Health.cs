using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
   [Header ("Health")]
   [SerializeField] private float startingHealth = 3;
   public float currentHealth { get; private set; }
   private Animator anim;
   private bool dead;
   public LogicManager logic;

   [Header("iFrames")]
   [SerializeField] private float iFramesDuration;
   [SerializeField] private int numberOfFlashes;
   private SpriteRenderer sprite;

   private void Awake()
   {
      currentHealth = startingHealth;
      anim = GetComponent<Animator>();
      sprite = GetComponent<SpriteRenderer>();
   }

   public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else {
            if (!dead) {
               GetComponent<PlayerMovement>().enabled = false;
               dead = true;
               anim.SetTrigger("dead");
               logic.gameOver();
            }
        }
   }

   public void AddHealth(float _health, GameObject HealthCollectible) {
      if (currentHealth != 0 && currentHealth != startingHealth) {
         currentHealth += 1;
         HealthCollectible.SetActive(false);
      }
   }

   private IEnumerator Invulnerability() {
      Physics2D.IgnoreLayerCollision(9, 10, true);
      for (int i = 0; i < numberOfFlashes; i++) {
         sprite.color = new Color(1, 0, 0, 0.5f);
         yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
         sprite.color = Color.white;
         yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
      }
      Physics2D.IgnoreLayerCollision(9, 10, false);
   }
}
