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
    private Health playerHealth;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    //References
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private EnemyPatroll enemyPatroll;

    private void Awake() {
        anim = GetComponent<Animator>();
        enemyPatroll = GetComponentInParent<EnemyPatroll>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0) {
            if (PlayerInSight()) {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if (enemyPatroll != null) {
            enemyPatroll.enabled = !PlayerInSight();  
        }
    }

    private bool PlayerInSight() {
        if (playerHealth.currentHealth <= 0) {
            return false;
        }

        RaycastHit2D hit = Physics2D.BoxCast(hitbox.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(hitbox.bounds.size.x * range, hitbox.bounds.size.y, hitbox.bounds.size.z), 
        0, Vector2.left, 0, playerLayer);

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
