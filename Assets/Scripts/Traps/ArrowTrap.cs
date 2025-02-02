using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Attack() {
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
            Attack();
            cooldownTimer = 0;
        }
    }
}
