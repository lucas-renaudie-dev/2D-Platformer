using UnityEngine;

public class Health : MonoBehaviour
{
   private float startingHealth = 3;
   private float currentHealth;

   private void Awake()
   {
      currentHealth = startingHealth;
   }

   private void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) {
             
        }
        else {
            
        }
   }
}
