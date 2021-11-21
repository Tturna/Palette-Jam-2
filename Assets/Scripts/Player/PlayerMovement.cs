using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float sprintSpeed;
    float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    private bool isFacingRight;
    private SpriteRenderer playerSprite;
    bool isSprinting;
    [HideInInspector]
    public bool canMove = true;

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
        if (canMove)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movement = moveInput.normalized * moveSpeed;  

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
            playerSprite.gameObject.transform.localScale = new Vector3(1f, 1f,1f);
        }
        if(!isFacingRight)
        {
            playerSprite.gameObject.transform.localScale = new Vector3(-1f, 1f,1f);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            moveSpeed = sprintSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            moveSpeed = speed;
        }

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), Mathf.Lerp(camTarget.localPosition.y, aheadAmount * Input.GetAxisRaw("Vertical"), aheadSpeed * Time.deltaTime), camTarget.localPosition.z);
        }

        if(ItemManager.instance.isCarrying)
        {
            animator.SetBool("isHolding", true);
        }else
        {
            animator.SetBool("isHolding", false);
        }   
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);      
    }
}
