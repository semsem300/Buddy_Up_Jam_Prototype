using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] int Pattern1AttackDamage = 20;
    [SerializeField] int Pattern2AttackDamage = 40;
    [SerializeField] int Pattern3AttackDamage = 40;
    [SerializeField] int Pattern4AttackDamage = 40;
    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask;
    public void Pattern1Attack()
    {
        Vector3 attackPos = transform.position;
        attackPos += transform.right * attackOffset.x;
        attackPos += transform.up * attackOffset.y;
        Collider2D collider2D = Physics2D.OverlapCircle(attackPos, attackRange, attackMask);
        if (collider2D != null)
        {
            if (collider2D.CompareTag("Player"))
            {
                Debug.Log("GetDamage");
                collider2D.GetComponent<Health>().AddDamage(Pattern1AttackDamage);
            }
        }
    }

}
