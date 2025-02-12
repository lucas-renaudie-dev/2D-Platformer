using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead")] 
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    [Header("Sound")]
    [SerializeField] private AudioClip impactSound;

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
            if (checkTimer >= checkDelay) {
                CheckForPlayer();
                if (Vector3.Distance(transform.position, destination) <= range) {
                    destination = -destination;
                }
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
                Debug.Log("destination: " + i);
                Debug.Log("direction: " + directions[i]);
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
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(other);
        Stop();
    }

    public void ResetTrap() {
        checkTimer = checkDelay;
        attacking = false;
    }
}
