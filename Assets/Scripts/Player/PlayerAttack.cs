using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint2;
    public GameObject[] fireballs;

    public float attackCooldown = 0.25f;
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
        SoundManager.instance.PlaySound(fireballSound);
        int x = FindFireball();
        fireballs[x].transform.position = firePoint.position;
        fireballs[x].GetComponent<Projectile>().SetDirection(Mathf.Sign(-transform.localScale.x));
    }

    private void Attack2() {
        SoundManager.instance.PlaySound(fireballSound);
        int x = FindFireball();
        fireballs[x].transform.position = firePoint2.position;
        fireballs[x].GetComponent<Projectile>().SetDirection(0);
    }

    private int FindFireball() {
        for (int i = 0; i < fireballs.Length; i++) {
            if (!fireballs[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }
}
