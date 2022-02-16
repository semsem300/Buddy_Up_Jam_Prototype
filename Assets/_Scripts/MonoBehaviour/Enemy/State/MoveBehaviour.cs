using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    Rigidbody2D rb;
    private Transform target;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float speed;
    [SerializeField] float maxRange;
    [SerializeField] float minRange;
    [SerializeField] Vector3 stopDistence = new Vector3(1, 1, 0);
    [SerializeField] float moveWaitTime = 1f;
    [SerializeField] float startMoveWaitTime = 1f;
    int randomPoint;
    bool isAttacking;
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
        if (Vector3.Distance(target.position, rb.position) <= maxRange && Vector3.Distance(target.position, rb.position) >= minRange)
            FollowPlayer(animator);
        else
            Patrol(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
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
                , patrolPoints[randomPoint].position, speed * Time.fixedDeltaTime);
        }
        else
        {
            if (moveWaitTime <= 0)
            {
                animator.SetBool("IsMoving", false);
                randomPoint = Random.Range(0, patrolPoints.Length);
                moveWaitTime = startMoveWaitTime;
            }
            else
            {
                moveWaitTime -= Time.fixedDeltaTime;
            }

        }
    }
    void FollowPlayer(Animator animator)
    {
        animator.SetBool("IsMoving", true);
        animator.SetFloat("Horizontal", (target.position.x - rb.position.x));
        animator.SetFloat("Vertical", (target.position.y - rb.position.y));
        rb.position =
            Vector3.MoveTowards(rb.position, target.position - stopDistence, speed * Time.fixedDeltaTime);
        if (Mathf.Abs(Vector2.Distance(target.position, rb.position)) < 1f)
        {
            Attack(animator);
        }
        else
        {
            isAttacking = false;
            animator.SetBool("IsMoving", true);
            rb.position = Vector2.MoveTowards(rb.position, target.position - stopDistence, speed * Time.fixedDeltaTime);
        }
    }
    void Attack(Animator animator)
    {
        isAttacking = true;
        //animator.SetBool("IsMoving", false);
        animator.SetTrigger("Attack");
        Debug.Log("Attack");
    }

}
