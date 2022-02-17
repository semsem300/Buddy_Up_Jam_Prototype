using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Vector3 attackOffset;
    [SerializeField] Enemy Enemy;
    public void Pattern1Attack()
    {
        var attackPos = transform.position;
        attackPos += transform.right * attackOffset.x;
        attackPos += transform.up * attackOffset.y;
        Collider2D collider2D = Physics2D.OverlapCircle(attackPos, Enemy.attackRange, Enemy.attackMask);
        if (collider2D != null)
        {
            if (collider2D.CompareTag("Player"))
            {
                Debug.Log("GetDamage");
                collider2D.GetComponent<PlayerHealth>().TakeDamage(Enemy.Pattern1AttackDamage);
            }
        }
    }
   
}
