using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    [SerializeField] Player player;

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
        player.position = body.position;
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            MovmentInput();
            DashInput();
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.ResetTrigger("Dash");
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            Move();
        }

    }
    #endregion
    #region Custome Func

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
            AudioManager.Instance.PlaySoundFxSource(player.moveClip);
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
        player.position = body.position;
    }
    void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f))
        {
            AudioManager.Instance.PlaySoundFxSource(player.dodgeRollClip);
            StartCoroutine(Dash(horizontal, vertical));
            animator.SetTrigger("Dash");
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
        CameraShake.Instance.ShakeIt(5f, .2f);
        yield return new WaitForSeconds(player.dashtime);
        body.MovePosition(body.position + new Vector2(player.dashDistance * dirx, player.dashDistance * diry));
        player.isDash = false;
    }
    #endregion
}
