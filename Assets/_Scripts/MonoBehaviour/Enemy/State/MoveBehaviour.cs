using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    Rigidbody2D rb;
    private Transform target;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Enemy enemy;
    int randomPoint;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerMovement>().transform;
        randomPoint = 1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (Vector3.Distance(target.position, rb.position) <= enemy.maxDetectRange && Vector3.Distance(target.position, rb.position) >= enemy.minDetectRange)
                FollowPlayer(animator);
            else
                Patrol(animator);
        }
        else
        {
            animator.ResetTrigger("Attack01");
            animator.ResetTrigger("Attack02");
            animator.ResetTrigger("Attack03");
            animator.ResetTrigger("Attack04");
            animator.SetBool("IsMoving", false);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack01");
        animator.ResetTrigger("Attack02");
        animator.ResetTrigger("Attack03");
        animator.ResetTrigger("Attack04");
    }
    private void Patrol(Animator animator)
    {
        var dis = Vector3.Distance(target.position, patrolPoints[randomPoint].position);
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
    void FollowPlayer(Animator animator)
    {
        animator.SetBool("IsMoving", true);
        animator.SetFloat("Horizontal", (target.position.x - rb.position.x));
        animator.SetFloat("Vertical", (target.position.y - rb.position.y));
        rb.position =
            Vector3.MoveTowards(rb.position, target.position - enemy.stopDistence, enemy.speed * Time.fixedDeltaTime);
        if (Mathf.Abs(Vector2.Distance(target.position, rb.position)) < 1f)
        {
            Attack(animator);
        }
        else
        {
            animator.SetBool("IsMoving", true);
            rb.position = Vector2.MoveTowards(rb.position, target.position - enemy.stopDistence, enemy.speed * Time.fixedDeltaTime);
        }
    }
    void Attack(Animator animator)
    {
        //animator.SetBool("IsMoving", false);
        switch (enemy.currentPattern)
        {
            case AttackPattern.Pattern1:
                animator.SetTrigger("Attack01");
                animator.ResetTrigger("Attack02");
                animator.ResetTrigger("Attack03");
                animator.ResetTrigger("Attack04");
                enemy.ChangeAttackPattern();
                break;
            case AttackPattern.Pattern2:
                animator.ResetTrigger("Attack01");
                animator.SetTrigger("Attack02");
                animator.ResetTrigger("Attack03");
                animator.ResetTrigger("Attack04");
                enemy.ChangeAttackPattern();
                break;
            case AttackPattern.Pattern3:
                animator.ResetTrigger("Attack01");
                animator.ResetTrigger("Attack02");
                animator.SetTrigger("Attack03");
                animator.ResetTrigger("Attack04");
                enemy.ChangeAttackPattern();
                break;
            case AttackPattern.Pattern4:
                animator.ResetTrigger("Attack01");
                animator.ResetTrigger("Attack02");
                animator.ResetTrigger("Attack03");
                animator.SetTrigger("Attack04");
                enemy.ChangeAttackPattern();
                break;
        }

        Debug.Log("Attack");
    }

}
