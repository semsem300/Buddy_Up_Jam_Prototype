using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack01State : MonoBehaviour
{
    int randomPoint;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    Rigidbody2D rb;
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (enemy.currentPattern1AttackTime > 0)
            {
                //if (Mathf.Abs(Vector2.Distance(player.position - enemy.Attack01StopDistence, transform.position)) > .1f && enemy.currentPattern == AttackPattern.Pattern1)
                //{
                //    transform.position = Vector2.MoveTowards(transform.position, player.position - enemy.Attack01StopDistence, enemy.speed * Time.fixedDeltaTime);
                //}
            }
        }
    }
    public void FirstPatternStrategy()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (enemy.currentPattern1AttackTime > 0)
            {
                if (Vector3.Distance(player.position, rb.position) <= enemy.maxDetectRange && Vector3.Distance(player.position, rb.position) >= enemy.minDetectRange)
                    FollowPlayer(animator, rb, player.position);
                else
                    Patrol(animator, rb, player.position);
                enemy.currentPattern1AttackTime -= Time.deltaTime;

            }
            else
            {
                // enemy.currentPattern = AttackPattern.Pattern1;
                //enemy.ChangeAttackPattern();
                enemy.currentPattern1AttackTime = enemy.Pattern1AttackTime;
            }
        }
    }
    private void Patrol(Animator animator, Rigidbody2D rb, Vector3 targetposition)
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            var dis = Vector3.Distance(targetposition, patrolPoints[randomPoint].position);
            var distx = patrolPoints[randomPoint].position.x - rb.position.x;
            var disty = patrolPoints[randomPoint].position.y - rb.position.y;
            if ((Mathf.Abs(distx) > 1f && Mathf.Abs(distx) < 5f) ||
                (Mathf.Abs(disty) > 1f && Mathf.Abs(disty) < 5f))
            {
                animator.SetBool("IsMoving", true);
                animator.SetFloat("Horizontal", distx);
                animator.SetFloat("Vertical", disty);
                rb.position = Vector3.MoveTowards(rb.position
                    , patrolPoints[randomPoint].position, enemy.speed * Time.fixedDeltaTime);
            }
            else
            {
                if (enemy.moveWaitTime <= 0)
                {
                    animator.SetBool("IsMoving", false);
                    randomPoint = Random.Range(0, patrolPoints.Length);
                    enemy.moveWaitTime = enemy.startMoveWaitTime;
                }
                else
                {
                    enemy.moveWaitTime -= Time.fixedDeltaTime;
                }

            }
        }
    }
    void FollowPlayer(Animator animator, Rigidbody2D rb, Vector3 targetposition)
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            AudioManager.Instance.PlaySoundFxSource(enemy.movingClip);
            animator.SetBool("IsMoving", true);
            animator.SetFloat("Horizontal", (targetposition.x - rb.position.x));
            animator.SetFloat("Vertical", (targetposition.y - rb.position.y));
            if (enemy.attack01CoolTime >= enemy.maxAttack01Cooltime)
            {
                rb.position =
                    Vector3.MoveTowards(rb.position, targetposition - enemy.Attack01StopDistence, enemy.speed * Time.fixedDeltaTime);

                Attack(animator);
                enemy.attack01CoolTime -= Time.deltaTime;
            }
            else if (enemy.attack01CoolTime < enemy.maxAttack01Cooltime)
            {
                animator.ResetTrigger("Attack01");
                animator.ResetTrigger("Attack02");
                animator.ResetTrigger("Attack03");
                animator.ResetTrigger("Attack04");
                if (enemy.attack01CoolTime <= 0)
                {
                    enemy.attack01CoolTime = enemy.maxAttack01Cooltime;
                }
                else
                    enemy.attack01CoolTime -= Time.deltaTime;
            }
        }
    }
    void Attack(Animator animator)
    {
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Attack01");
        animator.ResetTrigger("Attack02");
        animator.ResetTrigger("Attack03");
        animator.ResetTrigger("Attack04");
        StartCoroutine(WaitBetweenAttack(enemy.currentPattern1AttackTime));
        enemy.ChangeAttackPattern();
    }
    IEnumerator WaitBetweenAttack(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
