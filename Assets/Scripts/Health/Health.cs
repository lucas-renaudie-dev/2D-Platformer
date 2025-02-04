using System.Collections;
using Unity.VisualScripting;
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

   [Header("Components")]
   [SerializeField] private Behaviour[] components;

   [Header("Sounds")]
   [SerializeField] private AudioClip deathSound;
   [SerializeField] private AudioClip hurtSound;

   private void Awake()
   {
      currentHealth = startingHealth;
      anim = GetComponent<Animator>();
      sprite = GetComponent<SpriteRenderer>();
   }

   public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) {
            //anim.SetBool("Grounded", true); uncomment if want a seperate animation for hurt
            StartCoroutine(InvulnerabilityHurt());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else {
            if (!dead) {
               //TODO: Deactivate all attached component classes ()

               foreach (Behaviour component in components) {
                  component.enabled = false;
               }

               if (gameObject.tag == "Player") { //Player death
                  anim.SetBool("Grounded", true);
                  anim.SetTrigger("dead");
                  dead = true;
                  SoundManager.instance.PlaySound(deathSound);


                  if (GetComponent<PlayerRespawn>().checkpointExists) {
                     GetComponent<PlayerRespawn>().StartRespawn();
                  }
                  else {
                     //logic.gameOver();
                  }
               }

               else { //Enemy death
                  anim.SetTrigger("dead");
                  dead = true;
                  SoundManager.instance.PlaySound(deathSound);
               }
            }
        }
   }

   public void AddHealth(float _health, GameObject HealthCollectible) {
      if (currentHealth != 0 && currentHealth != startingHealth && HealthCollectible != null) { //Health pickup case
         currentHealth += 1;
         HealthCollectible.SetActive(false);
      }
      else if (dead == true && HealthCollectible == null) { //Respawn case
         currentHealth = _health;
      }
   }

   private IEnumerator InvulnerabilityHurt() { //invulnerability after taking damage
      //anim.SetBool("hurt", true);
      Physics2D.IgnoreLayerCollision(9, 10, true);
      for (int i = 0; i < numberOfFlashes; i++) {
         sprite.color = new Color(1, 0, 0, 0.5f);
         yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
         sprite.color = Color.white;
         yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
      }
      Physics2D.IgnoreLayerCollision(9, 10, false);
      //anim.SetBool("hurt", false);
   }

   private IEnumerator InvulnerabilityRespawn() { //invulnerability after respawn
      GetComponent<PlayerMovement>().enabled = false;
      Physics2D.IgnoreLayerCollision(9, 10, true);
      for (int i = 0; i < numberOfFlashes; i++) {
         sprite.color = new Color(1, 0, 0, 0.5f);
         yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
         sprite.color = Color.white;
         yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
      }
      Physics2D.IgnoreLayerCollision(9, 10, false);
      GetComponent<PlayerMovement>().enabled = true;
   }

   public void Respawn() {
      AddHealth(startingHealth, null);
      dead = false;

      anim.ResetTrigger("dead");
      anim.Play("Idle");
      anim.SetBool("Grounded", true);
      anim.SetBool("run", false);
      StartCoroutine(InvulnerabilityRespawn()); //optional

      //TODO: Activate all attached component classes (checkpoints vid, 7:45)
      //GetComponent<PlayerMovement>().enabled = true;
   }

   private void Deactivate() {
      gameObject.SetActive(false);
   }
}
