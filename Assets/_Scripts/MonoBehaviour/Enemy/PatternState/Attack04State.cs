using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack04State : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    [SerializeField] Vector2 center = new Vector2(-4.5f, 0);
  
    Rigidbody2D rb;
   
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.State == GameState.Playing)
        //{
        //    if (enemy.currentPattern4AttackTime > 0)
        //    {
        //        if (Vector2.Distance(transform.position, center) < 0.1f)
        //        {
        //            rb.velocity = Vector2.zero;
        //        }
        //        // Moves towards the center of the room
        //        rb.position =
        //               Vector3.MoveTowards(rb.position, center, enemy.speed * Time.fixedDeltaTime);
        //    }
        //}
    }

    public void FourthPatternStrategy()
    {
        enemy.currentPattern = AttackPattern.Pattern3;
        //if (enemy.currentPattern4AttackTime > 0)
        //{

        //    // center and look to player
        //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //    // Stops and starts aiming to the player
        //    rb.velocity = Vector2.zero;
        //    // Locks on the player
        //    animator.SetFloat("Horizontal", (player.position.x - transform.position.x));
        //    animator.SetFloat("Vertical", (player.position.y - transform.position.y));
        //    // trigger attack Animation
        //    if (enemy.attack04CoolTime >= enemy.maxAttack04Cooltime)
        //    {
        //        Attack();
        //        // Shoots slow bullets towards the player (Game obj contain script to move towards the player)
        //        enemy.attack04CoolTime -= Time.deltaTime;
        //    }
        //    else if (enemy.attack04CoolTime < enemy.maxAttack04Cooltime)
        //    {
        //        if (enemy.attack04CoolTime <= 0)
        //        {
        //            enemy.attack04CoolTime = enemy.maxAttack04Cooltime;
        //        }
        //        else
        //            enemy.attack04CoolTime -= Time.deltaTime;
        //    }


        //    enemy.currentPattern4AttackTime -= Time.deltaTime;
        //}
        //else if (enemy.currentPattern4AttackTime <= 0)
        //{
        //    enemy.currentPattern = AttackPattern.Pattern4;
        //    // enemy.ChangeAttackPattern();
        //    enemy.currentPattern4AttackTime = enemy.Pattern4AttackTime;
        //}
    }
    //void Attack()
    //{
    //    animator.SetBool("IsMoving", false);
    //    switch (enemy.currentPattern)
    //    {
    //        case AttackPattern.Pattern1:
    //            animator.SetTrigger("Attack01");
    //            StartCoroutine(WaitBetweenAttack(enemy.currentPattern1AttackTime));
    //            enemy.ChangeAttackPattern();
    //            break;
    //        case AttackPattern.Pattern2:
    //            StartCoroutine(GetComponent<Attack02State>().EndSoup());
    //            break;
    //        case AttackPattern.Pattern3:
    //            animator.SetTrigger("Attack03");
    //            StartCoroutine(WaitBetweenAttack(enemy.currentPattern3AttackTime));
    //            enemy.ChangeAttackPattern();
    //            break;
    //        case AttackPattern.Pattern4:
    //            animator.SetTrigger("Attack04");
    //            StartCoroutine(WaitBetweenAttack(enemy.currentPattern4AttackTime));
    //            enemy.ChangeAttackPattern();
    //            break;
    //    }
    //    Debug.Log("Attack");
    //}
    //IEnumerator WaitBetweenAttack(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //}
}
