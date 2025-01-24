using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePoint2;
    public GameObject[] fireballs;

    public float attackCooldown = 1;
    private float timer = 100000;
    private PlayerMovement movement;

    void Awake() {
        movement = GetComponent<PlayerMovement>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && timer > attackCooldown) {
            timer = 0;            
            Attack1();
        }
        if (Input.GetMouseButton(1) && !movement.grounded && timer > attackCooldown) {
            timer = 0;
            Attack2();
        }

        else {
            timer += Time.deltaTime;
        }
    }

    private void Attack1() {
        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(-transform.localScale.x));
    }

    private void Attack2() {
        fireballs[0].transform.position = firePoint2.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(0);
    }
}
