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

   private UIManager uiManager;

   private void Awake()
   {
      string diff = DifficultyScript.Instance.currentDifficulty;
      if (diff == "IMPOSSIBLE" && tag == "Player")
         currentHealth = 1;
      else 
         currentHealth = startingHealth;

      anim = GetComponent<Animator>();
      sprite = GetComponent<SpriteRenderer>();
      uiManager = FindFirstObjectByType<UIManager>();
   }

   public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        SoundManager.instance.PlaySound(hurtSound);

        if (currentHealth > 0) {
            //anim.SetBool("Grounded", true); uncomment if want a seperate animation for hurt
            StartCoroutine(InvulnerabilityHurt());
        }
        else {
            if (!dead) {
               //TODO: Deactivate all attached component classes ()

               foreach (Behaviour component in components) {
                  component.enabled = false;
                  Debug.Log(component);
               }

               if (gameObject.tag == "Player") { //Player death
                  anim.SetBool("Grounded", true);
                  anim.SetTrigger("dead");
                  dead = true;

                  if (GetComponent<PlayerRespawn>().checkpointExists) {
                     SoundManager.instance.PlaySound(deathSound);
                     GetComponent<PlayerRespawn>().StartRespawn();
                  }
                  else {
                     Physics2D.IgnoreLayerCollision(9, 10, true);
                     uiManager.GameOver();
                  }
               }

               else { //Enemy death
                  DisableCollisionWithPlayer(GetComponent<Collider2D>());
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
      //gameObject.SetActive(false);
   }

   public void DisableCollisionWithPlayer(Collider2D enemyCollider) {
      Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
      Physics2D.IgnoreCollision(enemyCollider, playerCollider, true);
   }
}
