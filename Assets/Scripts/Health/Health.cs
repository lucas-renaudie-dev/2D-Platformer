using UnityEngine;

public class Health : MonoBehaviour
{
   private float startingHealth = 3;
   public float currentHealth { get; private set; }
   private Animator anim;
   private bool dead;
   public LogicManager logic;


   private void Awake()
   {
      currentHealth = startingHealth;
      anim = GetComponent<Animator>();
   }

   public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) {
            anim.SetTrigger("hurt");
            //iframes
        }
        else {
            if (!dead) {
               GetComponent<PlayerMovement>().enabled = false;
               dead = true;
               anim.SetTrigger("dead");
               //logic.gameOver();
            }
        }
   }
}
