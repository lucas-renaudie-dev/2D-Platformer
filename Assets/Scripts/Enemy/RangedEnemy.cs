using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D hitbox;

    [Header("Player")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Fireball Sound")]
    [SerializeField] private AudioClip fireballSound;

    //References
    private Animator anim;
    private Health playerHealth;
    private EnemyPatroll enemyPatroll;

    private void Awake() {
        anim = GetComponent<Animator>();
        enemyPatroll = GetComponentInParent<EnemyPatroll>();
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0) {
            if (PlayerInSight()) {
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }

        if (enemyPatroll != null) {
            enemyPatroll.enabled = !PlayerInSight();  
        }
    }

    private void RangedAttack() {
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer = 0;
        int tmp = FindFireball();
        fireballs[tmp].transform.position = firePoint.position;
        fireballs[tmp].GetComponent<EnemyProjectile>().ActivateProjectile();   
    }

    private int FindFireball() {
        for (int i = 0; i < fireballs.Length; i++) {
            if (!fireballs[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInSight() {
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
}
