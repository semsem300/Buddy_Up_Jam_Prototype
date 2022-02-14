using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    Vector2 movementDirection;

    [SerializeField] Player player;
    [SerializeField] GameObject Boost;
    float horizontal;
    float vertical;
    bool doubleJump = false;
    float doubleTapTime;
    KeyCode lastkeyCode;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        movementDirection = new Vector2(horizontal, vertical).normalized;
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
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
                StartCoroutine(Dash(0f, -1f));
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
                StartCoroutine(Dash(0f, 1f));
                animator.SetTrigger("DodgeDown");
            }
            else
                doubleTapTime = Time.time + player.dashCoolTime;

            lastkeyCode = KeyCode.D;

        }
    }

    void FixedUpdate()
    {
        Move();
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void Move()
    {
        body.velocity = movementDirection * player.speed * Time.fixedDeltaTime;
    }
    IEnumerator Dash(float dirx, float diry)
    {
        player.isDash = true;
        body.velocity = new Vector2(body.velocity.x, body.velocity.y);
        //Boost = Instantiate(player.dashParticle, transform.position, Quaternion.identity) as GameObject;
        //Boost.transform.parent = gameObject.transform;
        body.AddForce(new Vector2(player.dashDistance * dirx, player.dashDistance * diry), ForceMode2D.Impulse);
       // CameraShake.Instance.ShakeIt(5f, .2f);
        yield return new WaitForSeconds(player.dashtime);
        player.isDash = false;
        animator.SetTrigger("Dodge");
    }

}
