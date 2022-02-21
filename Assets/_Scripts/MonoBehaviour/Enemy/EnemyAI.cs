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
    [SerializeField] AudioSetting setting;
    Animator animator;
    Rigidbody2D rb;
    private Vector3 target;
    [SerializeField] Vector2 center = new Vector2(-4.5f, 0);
    LineRenderer lineRenderer;
    List<Vector2> points = new List<Vector2>();
    [SerializeField] Vector2 offset = new Vector2(-1f, -1f);
    [SerializeField] float damageforce = 20f;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = animator.GetComponent<Rigidbody2D>();
        target = player.position;
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
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
                    GetComponent<Attack01State>().FirstPatternStrategy();
                    break;
                case AttackPattern.Pattern2:
                    GetComponent<Attack02State>().SecondPatternStrategy();
                    break;
                case AttackPattern.Pattern3:
                    GetComponent<Attack03State>().ThirdPatternStrategy();
                    break;
                    //case AttackPattern.Pattern4:
                    //    //  enemy.enemyObj.GetComponent<Attack01State>().FirstPatternStrategy(animator, rb, target.position, rb.position, enemy);
                    //    GetComponent<Attack04State>().FourthPatternStrategy();
                    //    break;
            }
        }
    }
    public void Pattern1Attack()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            AudioManager.Instance.PlaySoundFxSource(enemy.MonsterAttack01Clip);
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
                    rb.velocity *= -damageforce;
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
    public void Pattern4Attack()
    {
        //if (GameManager.Instance.State == GameState.Playing)
        //{
        //    //RaycastHit2D hit = Physics2D.Raycast(center, player.position, Vector2.Distance(transform.position, player.position));
        //    Collider2D col = Physics2D.OverlapCircle(center, enemy.attackRangeAttack04);
        //    // Debug.(rb.position, hit.point, Color.red);
        //    Vector3[] points = new List<Vector3>() {
        //          center
        //            ,new Vector2( player.position.x+offset.x, player.position.y+offset.y)
        //        }.ToArray();
        //    // if it hit the player 
        //    lineRenderer.enabled = true;
        //    lineRenderer.SetPositions(points);

        //    if (col != null)
        //    {

        //        if (col.CompareTag("Player"))
        //        {
        //            lineRenderer.enabled = true;
        //            lineRenderer.SetPositions(points);


        //            // grap player toward boss 
        //            Rigidbody2D playerRb = col.gameObject.GetComponent<Rigidbody2D>();
        //            playerRb.position = Vector2.MoveTowards(playerRb.position, new Vector2(center.x - enemy.Attack04StopDistence.x, center.y - enemy.Attack04StopDistence.y), enemy.grapSpeed * Time.deltaTime);
        //            Debug.Log("FourthPattern");
        //            // the player disapperas for x time 
        //            //The player receives another amount of  damage, a % of which is  transferred to the boss
        //            //The player is then  thrown away in a random direction
        //        }
        //        else
        //        {
        //            //lineRenderer.enabled = false;
        //            //lineRenderer.SetPositions(points);
        //        }
        //    }
        //}
    }
    IEnumerator RayOrders()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            AudioManager.Instance.PlaySoundFxSource(enemy.MonsterAttack03Clip);
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
    }
    public void CanShoot()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            enemy.canShoot = true;
            AudioManager.Instance.PlaySoundFxSource(enemy.MonsterAttack02ProjectileClip);
            Instantiate(enemy.attack02Obj, transform.position, Quaternion.identity);
            enemy.canShoot = false;
        }
    }
}
