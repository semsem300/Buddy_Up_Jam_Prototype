using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack04State : MonoBehaviour
{
    [SerializeField] float distance = 20f;
    [SerializeField] float grapSpeed = 20f;
    [SerializeField] float offset = .5f;
    [SerializeField] int maxPoints = 3;
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    [SerializeField] Vector2 center = new Vector2(-4.5f, 0);
    List<Vector2> points = new List<Vector2>();
    Rigidbody2D rb;
    LineRenderer lineRenderer;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (enemy.currentPattern4AttackTime > 0)
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
        //    // raycast and line render 
        //    RaycastHit2D hit = Physics2D.Raycast(center, new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)), Vector2.Distance(transform.position, player.position));
        //    Debug.DrawRay(center, hit.point, Color.red);
        //    Vector3[] points = new List<Vector3>() {
        //            rb.position
        //            ,new Vector2( player.position.x, player.position.y+offset)
        //        }.ToArray();
        //    // if it hit the player 
        //    if (hit.collider != null)
        //    {

        //        if (hit.collider.CompareTag("Player"))
        //        {
        //            Vector2 hitPoint = hit.point;
        //            lineRenderer.enabled = true;
        //            lineRenderer.SetPositions(points);


        //            // grap player toward boss 
        //            Rigidbody2D playerRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        //            playerRb.position = Vector2.MoveTowards(playerRb.position, center, grapSpeed * Time.deltaTime);
        //            Debug.Log("FourthPattern");
        //            // the player disapperas for x time 
        //            //The player receives another amount of  damage, a % of which is  transferred to the boss
        //            //The player is then  thrown away in a random direction
        //        }

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
}
