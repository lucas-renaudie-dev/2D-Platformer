using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;
    public LevelComplete victory;
    private float cooldownTimer;

    [Header("Sound")]
    [SerializeField] private AudioClip arrowSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Attack() {
        SoundManager.instance.PlaySound(arrowSound);
        int tmp = FindArrow();
        arrows[tmp].transform.position = firepoint.position;
        arrows[tmp].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow() {
        for(int i = 0; i < arrows.Length; i++) {
            if(!arrows[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }

    private void Update() {
        if(cooldownTimer < attackCooldown) {
            cooldownTimer += Time.deltaTime;
        } else {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().currentHealth > 0 && !victory.signPassed) {
                Attack();
            }
            cooldownTimer = 0;
        }
    }

    public void ResetTrap() {
        for (int i = 0; i < arrows.Length; i++) {
            arrows[i].SetActive(false);
        }
    }
}
