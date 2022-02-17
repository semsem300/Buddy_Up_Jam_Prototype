using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Vector3 attackOffset;
    [SerializeField] Player player;
    public void Attack()
    {
        var attackPos = transform.position;
        attackPos += transform.right * attackOffset.x;
        attackPos += transform.up * attackOffset.y;
        Collider2D collider2D = Physics2D.OverlapCircle(attackPos, player.attackRange, player.attackMask);
        if (collider2D != null)
        {
            if (collider2D.CompareTag("Enemy"))
            {
                Debug.Log("GetDamage");
                collider2D.GetComponent<EnemyHealth>().TakeDamage(player.damage);
            }
        }
    }
}
