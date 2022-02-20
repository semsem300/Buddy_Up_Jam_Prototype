using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject enemyObj;
    [SerializeField] Vector3 attackOffset;
    [SerializeField] Enemy enemy;
    [SerializeField] Player player;
    Animator animator;
    Rigidbody2D rb;
    private Vector3 target;
    [SerializeField] Vector2 center = new Vector2(-4.5f, 0);
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = animator.GetComponent<Rigidbody2D>();
        target = player.position;
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (GameManager.Instance.State == GameState.Playing)
            {
                enemy.canShoot = false;
                PatternStrategies();
            }
            else
            {
                animator.ResetTrigger("Attack01");
                animator.ResetTrigger("Attack02");
                animator.ResetTrigger("Attack03");
                animator.ResetTrigger("Attack04");

            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
    void PatternStrategies()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            switch (enemy.currentPattern)
            {

                case AttackPattern.Pattern1:
                    GetComponent<Attack01State>().FirstPatternStrategy(animator, rb, rb.position);
                    break;
                case AttackPattern.Pattern2:

                    //Debug.Log(Vector2.Distance(center, rb.position));
                    if (Mathf.Abs(Vector2.Distance(center, rb.position)) < 0.1f)
                    {
                        GetComponent<Attack02State>().SecondPatternStrategy(animator);
                    }
                    break;
                case AttackPattern.Pattern3:
                    GetComponent<Attack03State>().ThirdPatternStrategy(animator, center, target);
                    //  enemy.enemyObj.GetComponent<Attack01State>().FirstPatternStrategy(animator, rb, target.position, rb.position, enemy);
                    break;
                case AttackPattern.Pattern4:
                    //  enemy.enemyObj.GetComponent<Attack01State>().FirstPatternStrategy(animator, rb, target.position, rb.position, enemy);
                    GetComponent<Attack04State>().FourthPatternStrategy(animator, center, target);
                    break;
            }
        }
    }
    public void Pattern1Attack()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            var attackPos = transform.position;
            attackPos += transform.right * attackOffset.x;
            attackPos += transform.up * attackOffset.y;
            Collider2D collider2D = Physics2D.OverlapCircle(attackPos, enemy.attackRange, enemy.attackMask);
            if (collider2D != null)
            {
                if (collider2D.CompareTag("Player"))
                {
                    Debug.Log("GetDamage");
                    collider2D.GetComponent<PlayerHealth>().TakeDamage(enemy.Pattern1AttackDamage);
                    //     transform.position = Vector2.MoveTowards(transform.position, Enemy.position, Enemy.speed * Time.deltaTime);
                }
            }
        }
    }
    public void Pattern3Attack()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            StartCoroutine(RayOrders());
        }
    }
    IEnumerator RayOrders()
    {
        GameObject ray1 = Instantiate(enemy.attack03Obj, transform.position, Quaternion.identity);
        ray1.transform.parent = enemyObj.transform;
        GameObject ray2 = Instantiate(enemy.attack03Obj, transform.position, Quaternion.Euler(0, 0, 90));
        ray2.transform.parent = enemyObj.transform;
        Destroy(ray1, 1.5f);
        Destroy(ray2, 1.5f);
        yield return new WaitForSeconds(2);
        GameObject ray3 = Instantiate(enemy.attack03Obj, transform.position, Quaternion.Euler(0, 0, 45));
        ray3.transform.parent = enemyObj.transform;
        GameObject ray4 = Instantiate(enemy.attack03Obj, transform.position, Quaternion.Euler(0, 0, 135));
        ray4.transform.parent = enemyObj.transform;
        Destroy(ray3, 1.5f);
        Destroy(ray4, 1.5f);
        yield return new WaitForSeconds(2);
        GameObject ray5 = Instantiate(enemy.attack03Obj, transform.position, Quaternion.identity);
        ray5.transform.parent = enemyObj.transform;
        GameObject ray6 = Instantiate(enemy.attack03Obj, transform.position, Quaternion.Euler(0, 0, 90));
        ray6.transform.parent = enemyObj.transform;
        Destroy(ray5, 1.5f);
        Destroy(ray6, 1.5f);
        yield return new WaitForSeconds(2);
    }
    public void CanShoot()
    {
        enemy.canShoot = true;
        Instantiate(enemy.attack02Obj, transform.position, Quaternion.identity);
        enemy.canShoot = false;
    }
}
