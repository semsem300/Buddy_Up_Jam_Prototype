using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed;
    private Rigidbody2D rb;
    private bool moving;
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector2 moveDirection;

    private float axisChangeTime;
    private bool verticalGoesFirst;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            axisChangeTime -= Time.deltaTime;
           
            if (verticalGoesFirst)
            {
                rb.velocity = new Vector2(0, moveDirection.y);
            }
            else
            {
                rb.velocity = new Vector2(moveDirection.x, 0);
            }

            if (axisChangeTime <= 0f)
            {
                anim.SetFloat("Speed", moveDirection.normalized.sqrMagnitude);
                anim.SetFloat("Horizontal", moveDirection.normalized.x);
                anim.SetFloat("Vertical", moveDirection.normalized.y);
                verticalGoesFirst = !verticalGoesFirst;
            }

            if (timeToMoveCounter < 0f)
            {
                moving = false;
                anim.SetFloat("Speed", 0);
                timeBetweenMoveCounter = timeBetweenMove;
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = timeToMove;

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                verticalGoesFirst = Random.value >= 0.5f;
                if (verticalGoesFirst)
                {
                    axisChangeTime = (moveDirection.y / (moveDirection.x + moveDirection.y)) * timeToMove;
                }
                else
                {
                    axisChangeTime = (moveDirection.x / (moveDirection.x + moveDirection.y)) * timeToMove;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            moveDirection -= moveDirection;
            //new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);

        }
    }
    //    [SerializeField] private float speed = 10f;
    //    [SerializeField] private float m_Damage = 5f;
    //    [SerializeField] private float m_Health = 20f;
    //    [SerializeField] Player player;
    //    //[SerializeField] GameObject hittEffect;
    //    //[SerializeField] Slider HealthSlider;
    //    [SerializeField] float attackCoolTime = 10;
    //    [SerializeField] float maxAttackCooltime = 10;
    //    Rigidbody2D rb;
    //    // fliping 
    //    bool canFlip = true;
    //    [SerializeField] bool facingRight = false;

    //    Transform enemyTransform;
    //    Animator animator;
    //    [SerializeField] float detectDistince = 5f;
    //    [SerializeField] float waitTime;
    //    [SerializeField] float StartWaitTime;
    //    [SerializeField] Transform target;
    //    private int randomPoint;

    //    [SerializeField] public Transform[] waypoints;
    //    // [SerializeField] AudioClip hitClip;
    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        rb = GetComponent<Rigidbody2D>();
    //        enemyTransform = GetComponent<Transform>();
    //        animator = GetComponent<Animator>();
    //        waitTime = StartWaitTime;
    //        randomPoint = Random.Range(0, waypoints.Length - 1);
    //    }

    //    private Transform FindTarget()
    //    {
    //        if (target == null)
    //        {
    //            Transform _target = GameObject.FindGameObjectWithTag("Player").transform;
    //            if (_target == null) return null;
    //            else
    //            {
    //                float _dist = Vector2.Distance(_target.position, transform.position);
    //                if (_dist <= detectDistince)
    //                    return _target;
    //                else return null;
    //            }
    //        }
    //        else
    //        {
    //            float _dist = Vector2.Distance(target.position, transform.position);
    //            if (_dist <= detectDistince)
    //                return target;
    //            else return null;
    //        }
    //    }


    //    void Update()
    //    {
    //        //HealthSlider.value = m_Health;
    //        //if (Time.time > nextFlipChance)
    //        //{
    //        //    if (Random.Range(0, 10) >= 5)
    //        //    {
    //        //        flipFacing();
    //        //    }
    //        //    nextFlipChance = Time.time + fliptime;
    //        //}
    //        if (target != null)
    //        {
    //            //Vector2 _dir = target.position - transform.position;
    //            //_dir.Normalize();
    //            //animator.SetFloat("Horizontal", _dir.x);
    //            //animator.SetFloat("Vertical", _dir.y);
    //            // float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
    //            //  transform.LookAt(target.position, Vector3.back);
    //            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    //            //if (Mathf.Abs(Vector2.Distance(target.position, transform.position)) < 1f)
    //            //{
    //            //    animator.SetBool("Moveing", false);
    //            //    Debug.Log(Mathf.Abs(Vector2.Distance(target.position, transform.position)));
    //            //    StartCoroutine(Attack());
    //            //}
    //            //else
    //            //{
    //            //    animator.SetBool("Moveing", true);
    //            //    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //            //}
    //            Patrol();
    //        }
    //        else
    //        {
    //            Patrol();
    //        }
    //    }
    //    void flipFacing()
    //    {
    //        if (!canFlip) { return; }
    //        else
    //        {
    //            float facingX = enemyTransform.localScale.x;
    //            if (gameObject.transform.rotation.z == Mathf.Abs(180) || gameObject.transform.rotation.z == Mathf.Abs(0))
    //            {
    //                facingX *= -1;
    //                enemyTransform.localScale =
    //                    new Vector3(facingX, enemyTransform.localScale.y, enemyTransform.localScale.z);
    //            }
    //            else if (gameObject.transform.rotation.z == Mathf.Abs(90) || gameObject.transform.rotation.z == Mathf.Abs(270))
    //            {
    //                facingX *= -1;
    //                enemyTransform.localScale =
    //                    new Vector3(enemyTransform.localScale.y, facingX, enemyTransform.localScale.z);
    //            }

    //            facingRight = !facingRight;
    //        }
    //    }
    //    private void Patrol()
    //    {
    //        Vector2 _dir = transform.position;
    //        _dir.Normalize();
    //        rb.MovePosition(rb.position + new Vector2(waypoints[randomPoint].position.x * speed * Time.deltaTime, waypoints[randomPoint].position.y * speed * Time.deltaTime));
    //        //= Vector2.MoveTowards(transform.position, ); ;

    //        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
    //            new Vector2(waypoints[randomPoint].position.x, waypoints[randomPoint].position.y)) < 0.1f)
    //        {
    //            StartCoroutine(Idle());
    //            Debug.Log("current point" + randomPoint);
    //        }
    //        //transform.position = Vector2.MoveTowards(transform.position, waypoints[randomPoint].position, speed * Time.deltaTime);
    //        animator.SetFloat("Horizontal", rb.position.x);
    //        animator.SetFloat("Vertical", rb.position.y);
    //        animator.SetFloat("Speed", rb.velocity.sqrMagnitude * speed * Time.deltaTime);
    //    }

    //    public void AddDamge(float amount)
    //    {
    //        player.FinalScore++;
    //        m_Health -= amount;
    //        if (m_Health <= 0) Destroy(gameObject, .2f);
    //        // AudioManager.Instance.PlaySoundFxSource(hitClip);
    //    }
    //    IEnumerator Attack()
    //    {
    //        animator.SetBool("Moveing", false);
    //        if (attackCoolTime == maxAttackCooltime)
    //        {
    //            attackCoolTime -= Time.deltaTime;
    //            animator.SetTrigger("Attack");
    //            // GameObject effect = Instantiate(hittEffect, target.gameObject.transform.position, Quaternion.identity);
    //            //CameraShake.Instance.ShakeIt(10f, .2f);
    //            //Destroy(effect, 2f);
    //            yield return new WaitForSeconds(1f);
    //            player.AddDamage(m_Damage);
    //            rb.AddForce(new Vector2(-10, 0));
    //        }
    //        else if (attackCoolTime < maxAttackCooltime && attackCoolTime > 0) attackCoolTime -= Time.deltaTime;
    //        else if (attackCoolTime <= 0)
    //        {
    //            attackCoolTime = maxAttackCooltime;
    //        }
    //        yield return null;
    //    }

    //    IEnumerator Idle()
    //    {
    //        animator.SetFloat("Speed", 0);
    //        yield return new WaitForSeconds(waitTime);
    //        randomPoint = Random.Range(0, waypoints.Length - 1);
    //        animator.SetFloat("Speed", speed);
    //    }
    //    private void LateUpdate()
    //    {
    //        target = FindTarget();
    //    }

}
