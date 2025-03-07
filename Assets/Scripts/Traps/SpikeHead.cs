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

    [Header("Movement Type")]
    [SerializeField] private bool isHorizontal;
    [SerializeField] private bool isVertical;

    [Header("Level Complete Screen")]
    [SerializeField] LevelComplete levelCompleteSign;
    private Health playerHealth;

    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    private Vector3 previousLocation;

    private void OnEnable() {
        Stop();
    }

    private void Awake() {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); 
    }

    private void Start() {
        checkTimer = checkDelay;
    }

    private void Update() {
        if (playerHealth.currentHealth > 0 && !levelCompleteSign.signPassed) {
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
    }

    private void CalculateDirections() {
        if (isHorizontal) {
            directions[0] = transform.right * range;
            directions[1] = -transform.right * range;
        }
        else if (isVertical) {
            directions[0] = transform.up * range;
            directions[1] = -transform.up * range;
        }
        else {
            directions[0] = transform.right * range;
            directions[1] = -transform.right * range;
            directions[2] = transform.up * range;
            directions[3] = -transform.up * range;
        }
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
        previousLocation = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Compare tags
        bool isFireball    = other.CompareTag("Fireball");
        bool isCoin        = other.CompareTag("Coin");
        bool isCheckpoint  = other.CompareTag("Checkpoint");
        bool isLevelComp   = other.CompareTag("LevelComplete");
        bool isNextRoom    = other.CompareTag("NextRoomSign");
        bool isEnemy       = other.CompareTag("Enemy");
        bool isDoor        = other.CompareTag("Door");

        // Compare layers by using NameToLayer, which returns an int
        bool isGround      = other.gameObject.layer == LayerMask.NameToLayer("Ground");
        bool isWall        = other.gameObject.layer == LayerMask.NameToLayer("Wall");

        // If it's *not* any of those tags or layers, then do something
        if (!isFireball && !isCoin && !isCheckpoint && !isLevelComp && !isNextRoom && !isEnemy 
            && !isGround && !isWall && !isDoor) {
            if (transform.position != previousLocation) {
                SoundManager.instance.PlaySound(impactSound);
            }
            base.OnTriggerEnter2D(other);
            Stop();   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (transform.position != previousLocation) {
            SoundManager.instance.PlaySound(impactSound);
        }
        Stop(); 
    }

    public void ResetTrap() {
        checkTimer = checkDelay;
        attacking = false;
    }
}
