using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Enemy Enemy;
    private void Start()
    {
        Enemy.currentPattern = AttackPattern.Pattern1;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Enemy.attackRange);
    }
}
