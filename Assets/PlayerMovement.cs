using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    private bool isFacingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = speed;
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = moveInput.normalized * moveSpeed;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if(moveInput.x > 0)
        {
            isFacingRight = true;
        }

        if(moveInput.x < 0)
        {
            isFacingRight = false;
        }

        if(isFacingRight)
        {
            transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }else
        {
            transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed + 2f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = speed;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);      
    }
}
