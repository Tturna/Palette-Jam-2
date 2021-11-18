using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    private bool isFacingRight;
    private SpriteRenderer playerSprite;

    [Header("Animation")]
    public Animator animator;

    [Header("Camera")]
    public Transform camTarget;
    public float aheadAmount;
    public float aheadSpeed;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = speed;
        playerSprite = GetComponentInChildren<SpriteRenderer>();
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
            playerSprite.flipX = false;
        }
        if(!isFacingRight)
        {
            playerSprite.flipX = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed + 2f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = speed;
        }

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), Mathf.Lerp(camTarget.localPosition.y, aheadAmount * Input.GetAxisRaw("Vertical"), aheadSpeed * Time.deltaTime), camTarget.localPosition.z);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);      
    }
}
