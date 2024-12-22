using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    bool isFacingRight = true;
    [SerializeField] float player_speed = 4f;
    [SerializeField] float horizontalMovement;
    [SerializeField] float verticalMovement;
    [SerializeField] float jumpPower = 6.5f;
    public float pushSpeed;
    Rigidbody2D rb;
    PlayerController playerController;
    PlayerAttack playerAttack;

    //check ground
    public LayerMask groundMask;
    public Transform groundCheck;

    //check push
    public Transform checkRight;
    public LayerMask rockLayer;

    // bool trạng thái
    bool isJumping;
    bool isPushing;

    Animator anim;

    //platform move
    public bool isOnPlatform;
    public Rigidbody2D platformRb;
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public float HorizontalMovement { get => horizontalMovement; set => horizontalMovement = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
      //  playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleAnimation();
        DayNguocRock();

    }

   
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * player_speed, rb.velocity.y);
        if (isOnPlatform)
        {
            rb.velocity = new Vector2(horizontalMovement*player_speed + platformRb.velocity.x, platformRb.velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<float>();

    }
    public void Jump(InputAction.CallbackContext context) {
      
        if (IsGrounded() && context.performed  )
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(NameSound.Jump.ToString());
            }
            isOnPlatform = false;
          //  isJumping = true;
            if (anim != null)
            {
                anim.SetTrigger("TakeOff");
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        }

    }
    public void Shoot(InputAction.CallbackContext context) {
        if (context.performed) {
            
        }
    }

    public void Melee(InputAction.CallbackContext context) {
        if (context.performed)
        {

        }
    }
    private void HandleAnimation()
    {
        Flip();
      
        anim.SetBool("IsGrounded", IsGrounded());
        anim.SetBool("IsJumping", isJumping);
        anim.SetBool("IsPushing",IsPushing());
        anim.SetFloat("Speed",Mathf.Abs(horizontalMovement* player_speed));
        anim.SetFloat("VelocityY", rb.velocity.y);
    }
    void Flip()
    {
        if (isFacingRight && horizontalMovement < 0f || !isFacingRight && horizontalMovement > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public void SetJumpTrue()
    {
       // playerAttack.IsCompleted = true;
        isJumping = true;
       
    }
    public void SetJumpFalse()
    {
        isJumping = false;

    }
    private void IdleState()
    {
        if (anim != null)
        {
            anim.Play("Idle");
        }
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
    }
    public bool IsPushing()
    {
        return Physics2D.OverlapCircle(checkRight.position, 0.1f, rockLayer);
    }
    void DayNguocRock()
    {

        bool isPushWall = Physics2D.OverlapCircle(checkRight.position, 0.1f, groundMask);
        
        // Kiểm tra nếu người chơi đang đẩy vao tuong và đang đứng trên viên đá
        if (isPushWall && IsOnRock() && horizontalMovement != 0)
        {
            // Lấy tham chiếu đến viên đá (rock) mà người chơi đang đứng trên
            Collider2D rockCollider = Physics2D.OverlapCircle(groundCheck.position, 0.2f, rockLayer);
            if (rockCollider != null)
            {
                Rigidbody2D rockRb = rockCollider.GetComponent<Rigidbody2D>();
                if (rockRb != null)
                {
                    rockRb.velocity = new Vector2(-horizontalMovement * pushSpeed * player_speed, 0);
                }
            }

        }
    }

    bool IsOnRock()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, rockLayer);
    }
}
