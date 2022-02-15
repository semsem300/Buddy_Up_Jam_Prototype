using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    #region Fields
    [SerializeField] Player player;
    [SerializeField] AudioClip moveClip;
    Rigidbody2D body;
    Animator animator;
    GameObject Boost;
    float horizontal;
    float vertical;
    Vector2 movementDirection;
    float doubleTapTime;
    KeyCode lastkeyCode;
    #endregion
    #region Mono Func
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        MovmentInput();
        AttackInput();
        DashInput();
    }
    private void FixedUpdate()
    {
        Move();
    }
    #endregion
    #region Custome Func
    private void AttackInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void MovmentInput()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        movementDirection = new Vector2(horizontal, vertical).normalized;
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow)
            )
        {
            AudioManager.Instance.PlaySoundFxSource(moveClip);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
           Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) ||
           Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) ||
           Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow)
           )
        {
            AudioManager.Instance.StopSoundFxSource();
        }
    }
    private void Move()
    {
        body.velocity = movementDirection * player.speed * Time.fixedDeltaTime;
    }
    void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (doubleTapTime > Time.time && lastkeyCode == KeyCode.A)
            {
                // Dash
                StartCoroutine(Dash(-1f, 0f));
                animator.SetTrigger("DodgeLeft");
            }
            else
                doubleTapTime = Time.time + player.dashCoolTime;

            lastkeyCode = KeyCode.A;

        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            if (doubleTapTime > Time.time && lastkeyCode == KeyCode.D)
            {
                // Dash
                StartCoroutine(Dash(1f, 0f));
                animator.SetTrigger("DodgeRight");
            }
            else
                doubleTapTime = Time.time + player.dashCoolTime;

            lastkeyCode = KeyCode.D;

        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (doubleTapTime > Time.time && lastkeyCode == KeyCode.A)
            {
                // Dash
                StartCoroutine(Dash(0f, 1f));
                animator.SetTrigger("DodgeUp");
            }
            else
                doubleTapTime = Time.time + player.dashCoolTime;

            lastkeyCode = KeyCode.A;

        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            if (doubleTapTime > Time.time && lastkeyCode == KeyCode.D)
            {
                // Dash
                StartCoroutine(Dash(0f, -1f));
                animator.SetTrigger("DodgeDown");
            }
            else
                doubleTapTime = Time.time + player.dashCoolTime;

            lastkeyCode = KeyCode.D;

        }
    }
    IEnumerator Dash(float dirx, float diry)
    {
        player.isDash = true;
        body.velocity = new Vector2(body.velocity.x, body.velocity.y);
        //TODO Add Dash Effect & CameraShake
        //Boost = Instantiate(player.dashParticle, transform.position, Quaternion.identity) as GameObject;
        //Boost.transform.parent = gameObject.transform;
        // body.AddForce);
        // CameraShake.Instance.ShakeIt(5f, .2f);
        yield return new WaitForSeconds(player.dashtime);
        body.MovePosition(body.position + new Vector2(player.dashDistance * dirx, player.dashDistance * diry));
        player.isDash = false;
    }
    #endregion
}
