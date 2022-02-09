using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;


    Vector2 movementDirection;

    [SerializeField] Player player;
    float horizontal;
    float vertical;
    float rotaionSpeed = 90f;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        movementDirection = new Vector2(horizontal, vertical).normalized;

    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        body.velocity = movementDirection * player.speed * Time.fixedDeltaTime;
        Vector2 lookDir = movementDirection;
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        //Vector2 lookDir = movementDirection - body.position;
        //float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //body.rotation = angle;
    }


}
