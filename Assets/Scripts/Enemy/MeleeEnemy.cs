using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Collider")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D hitbox;

    [Header("Player")]
    [SerializeField] private LayerMask playerLayer;

    //References
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;
    private EnemyPatroll enemyPatroll;

    private void Awake() {
        anim = GetComponent<Animator>();
        enemyPatroll = GetComponentInParent<EnemyPatroll>();
    }
    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown) {
            if (PlayerInSight()) {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }

        if (enemyPatroll != null) {
            enemyPatroll.enabled = !PlayerInSight();  
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
