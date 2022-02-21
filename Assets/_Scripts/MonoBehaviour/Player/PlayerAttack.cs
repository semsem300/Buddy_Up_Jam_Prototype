using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Vector3 attackOffset;
    [SerializeField] Player player;
    [SerializeField] Vector2 attackForce = new Vector2(-50, -50);
    [SerializeField] Transform attackPoint;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            AttackInput();
        }
        else
        {
            animator.ResetTrigger("Attack");
        }
    }
    public void AttackTriggerd()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            Collider2D[] colliders2D = Physics2D.OverlapCircleAll(transform.position, player.attackRange, player.attackMask);
            foreach (var collider2D in colliders2D)
            {
                if (collider2D != null)
                {
                    if (collider2D.CompareTag("Enemy"))
                    {
                        Debug.Log("GetDamage");
                        collider2D.GetComponent<EnemyHealth>().TakeDamage(player.damage);
                        var hor = animator.GetFloat("Horizontal");
                        var ver = animator.GetFloat("Vertical");
                        collider2D.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce.x * hor, attackForce.y * ver));
                    }
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, player.attackRange);
    }
    private void AttackInput()
    {
        if (Input.GetButtonDown("Fire1") && (player.attackCoolTime >= player.maxAttackCooltime))
        {
            player.attackCoolTime -= Time.deltaTime;
            Attack();
        }
        else if (player.attackCoolTime < player.maxAttackCooltime)
        {
            animator.ResetTrigger("Attack");
            if (player.attackCoolTime <= 0)
            {
                player.attackCoolTime = player.maxAttackCooltime;
            }
            else
                player.attackCoolTime -= Time.deltaTime;
        }
    }
    private void Attack()
    {
        AudioManager.Instance.PlaySoundFxSource(player.attackClip);
        animator.SetTrigger("Attack");
    }
}
