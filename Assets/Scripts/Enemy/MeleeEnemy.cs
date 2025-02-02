using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;
    private Health playerHealth;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown) {
            if (PlayerInSight()) {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
    }

    private bool PlayerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(hitbox.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(hitbox.bounds.size.x * range, hitbox.bounds.size.y, hitbox.bounds.size.z), 
        0, Vector2.left, 0, playerLayer);
        
        if (hit.collider != null) {
            playerHealth = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitbox.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(hitbox.bounds.size.x * range, hitbox.bounds.size.y, hitbox.bounds.size.z));
    }

    private void DamagePlayer() {
        if (PlayerInSight()) {
            playerHealth.TakeDamage(damage);
        }
    }
}
