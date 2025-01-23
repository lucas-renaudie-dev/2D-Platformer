using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject[] fireballs;

    private Animator anim;
    public float attackCooldown = 1;
    private float timer = 100000;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && timer > attackCooldown) {
            Attack();
        }

        else {
            timer += Time.deltaTime;
        }
    }

    private void Attack() {
        timer = 0;

        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(-transform.localScale.x));
    }
}
