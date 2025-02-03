using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    private float resetTime = 5;
    private float lifetime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D coll;
    private void Awake() {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        gameObject.SetActive(true);
        hit = false;
        coll.enabled = true;
        lifetime = 0;
    }

    private void Update() {
        if (hit) {
            return;
        }

        float movementSpeed = speed * Time.deltaTime;

        //LEFT: ArrowTrap z rotation 0 and x scale -1
        //UP: ArrowTrap z rotation 90 and x scale 0
        //RIGHT: ArrowTrap z rotation 0 and x scale 0 
        //DOWN: ArrowTrap z rotation -90 and x scale 0
        transform.Translate(Vector3.right * movementSpeed);

        lifetime += Time.deltaTime;
        if (lifetime >= resetTime) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision); //execute logic from parent script first
        
        if (collision.tag != "Enemy" && collision.tag != "Fireball" && collision.tag != "Coin") {
            hit = true;
            coll.enabled = false;

            if (anim != null) {
                anim.SetTrigger("explode"); //fireball
            }
            else {
                gameObject.SetActive(false); //arrow
            }
        }
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }
}
