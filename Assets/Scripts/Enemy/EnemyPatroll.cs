using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    [Header("Patroll Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private bool isMovingLeft;

    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTime;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    private void Start() {
        isMovingLeft = true;
    }

    private void OnDisable() {
        anim.SetBool("moving", false);
    }

    private void Update() {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().currentHealth <= 0) {
            return;
        }

        if (isMovingLeft) {
            if (enemy.position.x > leftEdge.position.x) {
                MoveInDirection(-1);
            }
            else {
                DirectionChange();
            }
        }
        else {
            if (enemy.position.x < rightEdge.position.x) {
                MoveInDirection(1);
            }
            else {
                DirectionChange();
            }
        }
    }

    private void DirectionChange() {
        anim.SetBool("moving", false);

        idleTime += Time.deltaTime;

        if (idleTime >= idleDuration) {
            isMovingLeft = !isMovingLeft;
        }
    }

    private void MoveInDirection(int _direction) {
        idleTime = 0;

        anim.SetBool("moving", true);

        enemy.position = new Vector3(enemy.position.x + _direction * Time.deltaTime * speed, enemy.position.y, enemy.position.z);
    
        enemy.localScale = new Vector3(_direction, 1, 1);
    }

    public void ResetTrap() {
        idleTime = 0;
        isMovingLeft = true;
    }
}
