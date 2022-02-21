using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack02State : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject fireTransform;
    [SerializeField] Vector2 center = new Vector2(-4.5f, 0);
    Animator animator;
    Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (enemy.currentPattern2AttackTime > 0)
            {
                if (Vector2.Distance(transform.position, center) < 0.1f)
                {
                    rb.velocity = Vector2.zero;
                }
                // Moves towards the center of the room
                rb.position =
                       Vector3.MoveTowards(rb.position, center, enemy.speed * Time.fixedDeltaTime);
            }
        }
    }
    public void SecondPatternStrategy()
    {
        if (enemy.currentPattern2AttackTime > 0)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // Stops and starts aiming to the player
            rb.velocity = Vector2.zero;
            // Locks on the player
            animator.SetFloat("Horizontal", (player.position.x - transform.position.x));
            animator.SetFloat("Vertical", (player.position.y - transform.position.y));
            // trigger attack Animation
            if (enemy.attack02CoolTime >= enemy.maxAttack02Cooltime)
            {
                Attack(animator);
                // Shoots slow bullets towards the player (Game obj contain script to move towards the player)
                enemy.attack02CoolTime -= Time.deltaTime;
            }
            else if (enemy.attack02CoolTime < enemy.maxAttack02Cooltime)
            {
                if (enemy.attack02CoolTime <= 0)
                {
                    enemy.attack02CoolTime = enemy.maxAttack02Cooltime;
                }
                else
                    enemy.attack02CoolTime -= Time.deltaTime;
            }
            enemy.currentPattern2AttackTime -= Time.deltaTime;
        }
        else if (enemy.currentPattern2AttackTime <= 0)
        {
            // TODO Add  Wait time 
            // enemy.currentPattern = AttackPattern.Pattern2;
           // enemy.ChangeAttackPattern();
            enemy.currentPattern2AttackTime = enemy.Pattern2AttackTime;
        }
    }
    void Attack(Animator animator)
    {
        animator.SetBool("IsMoving", false);
        switch (enemy.currentPattern)
        {
            case AttackPattern.Pattern1:
                animator.SetTrigger("Attack01");
                StartCoroutine(WaitBetweenAttack(enemy.currentPattern1AttackTime));
                enemy.ChangeAttackPattern();
                break;
            case AttackPattern.Pattern2:
                StartCoroutine(EndSoup());
                break;
            case AttackPattern.Pattern3:
                StartCoroutine(EndFire());
                break;
            case AttackPattern.Pattern4:
                animator.SetTrigger("Attack04");
                StartCoroutine(WaitBetweenAttack(enemy.currentPattern4AttackTime));
                enemy.ChangeAttackPattern();
                break;
        }
        Debug.Log("Attack");
    }
    public IEnumerator EndFire()
    {
        animator.SetTrigger("Attack03");
        yield return new WaitForSeconds(enemy.Pattern3AttackTime);
       // enemy.currentPattern = AttackPattern.Pattern2;
         enemy.ChangeAttackPattern();
    }
    IEnumerator WaitBetweenAttack(float time)
    {
        yield return new WaitForSeconds(time);
    }
    public IEnumerator EndSoup()
    {
        animator.SetTrigger("Attack02");
        animator.SetBool("StartSoup", true);
        yield return new WaitForSeconds(enemy.Pattern2AttackTime);

        animator.SetBool("EndSoup", true);
        enemy.ChangeAttackPattern();

    }
}
