using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack03State : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            //if (enemy.currentPattern3AttackTime > 0)
            //{
            //    if (Mathf.Abs(Vector2.Distance(player.position - enemy.Attack03StopDistence, transform.position)) > 1f && enemy.currentPattern == AttackPattern.Pattern3)
            //    {
            //        transform.position = Vector2.MoveTowards(transform.position, player.position - enemy.Attack03StopDistence, enemy.speed * Time.fixedDeltaTime);
            //    }
            //}
        }
    }

    public void ThirdPatternStrategy()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (enemy.currentPattern3AttackTime > 0)
            {
                // Moves towards the player for x(very short)  amount of time
                // Starts charging an attack
                // Fires a four directional attack on x and y axis
                // Immediately follows up with another attack but this time at an angle of 45°
                //And again following x  and y axis
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                //  animator.SetBool("IsMoving", false);
                // Stops and starts aiming to the player
                rb.velocity = Vector2.zero;
                // Locks on the player
                animator.SetFloat("Horizontal", (player.position.x - transform.position.x));
                animator.SetFloat("Vertical", (player.position.y - transform.position.y));
                // trigger attack Animation
                if (enemy.attack03CoolTime >= enemy.maxAttack03Cooltime)
                {
                    if (Mathf.Abs(Vector2.Distance(player.position - enemy.Attack03StopDistence, transform.position)) > 1f && enemy.currentPattern == AttackPattern.Pattern3)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, player.position - enemy.Attack03StopDistence, enemy.speed * Time.fixedDeltaTime);
                    }
                    Attack(animator);
                    // Shoots slow bullets towards the player (Game obj contain script to move towards the player)
                    enemy.attack03CoolTime -= Time.deltaTime;
                }
                else if (enemy.attack03CoolTime < enemy.maxAttack03Cooltime)
                {
                    if (enemy.attack03CoolTime <= 0)
                    {
                        enemy.attack03CoolTime = enemy.maxAttack03Cooltime;
                    }
                    else
                        enemy.attack03CoolTime -= Time.deltaTime;
                }
            }
            else if (enemy.currentPattern3AttackTime <= 0)
            {
                // TODO Add  Wait time 

                //  enemy.currentPattern = AttackPattern.Pattern3;
                enemy.ChangeAttackPattern();
                enemy.currentPattern3AttackTime = enemy.Pattern3AttackTime;
            }
        }
    }
    void Attack(Animator animator)
    {
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Attack03");
        animator.ResetTrigger("Attack01");
        animator.ResetTrigger("Attack02");
        animator.ResetTrigger("Attack04");
        StartCoroutine(WaitBetweenAttack(enemy.currentPattern3AttackTime));
        enemy.ChangeAttackPattern();
    }
    IEnumerator WaitBetweenAttack(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
