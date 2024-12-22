using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D playerRb;
    Rigidbody2D rb;
    Vector3 moveDirection;

    [Header("Phạm vi di chuyển:")]
    public Transform pointStart;
    public Transform pointEnd;

    private Vector3 currentTarget;


    [Header("Đối tượng điều khiển:")]
    public Lever lever;


    [Header("Tốc độ di chuyển")]
    public float speed = 2f;
    [SerializeField]bool isMoving = false;


    private Animator anim;

    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

        if (pointEnd != null && pointStart != null)
        {
            if (lever == null)
                currentTarget = pointEnd.position;
            else currentTarget = pointStart.position; // nếu có điều khiển thì đặt currentTarget làm điểm bắt đầu
        }

    }
    private void FixedUpdate()
    {
        if ((pointStart == null && lever == null) || (pointEnd == null && lever == null)) return; // TH đứng im

        if (pointStart != null && pointEnd != null)
        {
            isMoving = true;
            if (lever == null)
            {
                //di chuyển tự động trong phạm vi A_B;
                MovingAuto();
                anim.SetBool("IsTurnOn", true);
            }
            else
            {
                // di chuyển theo điều khiển
                MovingByLever();
            }

        }
    }


   
    private void MovingByLever()
    {
        // Nếu lever đang bật
        if (lever.IsTurnOn)
        {
            anim.SetBool("IsTurnOn", true);
            moveDirection = (pointEnd.position - transform.position).normalized;  // Hướng di chuyển tới pointEnd
            rb.velocity = moveDirection * speed;  // Đặt vận tốc cho Rigidbody2D
        }
        else // Lever không bật, quay về vị trí ban đầu (currentTarget)
        {
            anim.SetBool("IsTurnOn", true);
            moveDirection = (currentTarget - transform.position).normalized;  // Hướng di chuyển về currentTarget
            rb.velocity = moveDirection * speed;  // Đặt vận tốc cho Rigidbody2D
        }

        // Kiểm tra nếu gần đến target, dừng chuyển động
        if (Vector2.Distance(transform.position, currentTarget) < 0.1f && !lever.IsTurnOn || Vector2.Distance(transform.position, pointEnd.position) < 0.1f && lever.IsTurnOn )
        {
            anim.SetBool("IsTurnOn", false);
            rb.velocity = Vector2.zero;  // Dừng vận tốc khi gần đến điểm đích
        }
    }


    private void MovingAuto()
    {
        Direction();

        // Đặt vận tốc cho Rigidbody2D
        rb.velocity = moveDirection * speed;

        if (Vector2.Distance(transform.position, currentTarget) < 0.05f)
        {
            currentTarget = currentTarget == pointStart.position ? pointEnd.position : pointStart.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isMoving)
        {
            if (playerMovement)
            {
                playerMovement.isOnPlatform = true;
                playerMovement.platformRb = rb;
            }
          

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isMoving)
        {
           if(playerMovement)
            playerMovement.isOnPlatform = false;
        }
    }

    void Direction()
    {
        moveDirection = (currentTarget - transform.position).normalized;
    }
}
