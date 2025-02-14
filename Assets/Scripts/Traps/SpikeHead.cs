using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead")] 
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    [Header("Sound")]
    [SerializeField] private AudioClip impactSound;

    [Header("Thresholds")]
    [SerializeField] private float leftWall;
    [SerializeField] private float rightWall;
    [SerializeField] private float ceiling;
    [SerializeField] private float ground;

    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    private void OnEnable() {
        Stop();
    }

    private void Start() {
        checkTimer = checkDelay;
    }

    private void Update() {
        if (attacking) {
            transform.Translate(destination * speed * Time.deltaTime);
        }
        else {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay) {
                CheckForPlayer();
            }
        }
    }

    private void CalculateDirections() {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void CheckForPlayer() {
        CalculateDirections();
        for (int i=0; i<directions.Length; i++) {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
        
            if (hit.collider != null && !attacking) {
                destination = directions[i];
                attacking = true;
                checkTimer = 0;
            }
        }
    }

    private void Stop() {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Compare tags
        bool isFireball    = other.CompareTag("Fireball");
        bool isCoin        = other.CompareTag("Coin");
        bool isCheckpoint  = other.CompareTag("Checkpoint");
        bool isNextRoom    = other.CompareTag("NextRoomSign");
        bool isEnemy       = other.CompareTag("Enemy");
        bool isDoor        = other.CompareTag("Door");

        // Compare layers by using NameToLayer, which returns an int
        bool isGround      = other.gameObject.layer == LayerMask.NameToLayer("Ground");
        bool isWall        = other.gameObject.layer == LayerMask.NameToLayer("Wall");

        // If it's *not* any of those tags or layers, then do something
        if (!isFireball && !isCoin && !isCheckpoint && !isNextRoom && !isEnemy 
            && !isGround && !isWall && !isDoor) {
            SoundManager.instance.PlaySound(impactSound);
            base.OnTriggerEnter2D(other);
            Stop();
        }
    }

    public void ResetTrap() {
        checkTimer = checkDelay;
        attacking = false;
    }
}
