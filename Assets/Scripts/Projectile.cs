
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float attackSpeed = 10;
    private bool hit;
    private float direction;
    private bool isVerticalAttack;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;

        if (isVerticalAttack) {
            float movementSpeed = attackSpeed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);
        }
        else {
            float movementSpeed = attackSpeed * Time.deltaTime * direction;
            transform.Translate(movementSpeed, 0, 0);
        }

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") {
            hit = true;
            boxCollider.enabled = false;
        
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
            anim.SetTrigger("explode");
        }
    }

    public void SetDirection(float _direction) {
        lifetime = 0;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;


        if (_direction != 0) {
            isVerticalAttack = false;
            direction = _direction;

            transform.localScale = new Vector3(_direction * 0.7f, transform.localScale.y, transform.localScale.z);
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
        }
        else {
            isVerticalAttack = true;
            direction = 1;
            transform.localScale = new Vector3(0.7f, transform.localScale.y, transform.localScale.z);
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, -90);
        }
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }
}
