using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAuto : MonoBehaviour
{
    public float speed = 2f;
    public bool canFlip = false;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    Vector3 currentTarget;
    public int damage = 100;
    Animator anim;
    bool isFacingRight = true;
    bool isMoveAuto = true;

    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        if(endPoint == null || startPoint== null) isMoveAuto =false;
        if(isMoveAuto)
            currentTarget = endPoint.position;
    }
    private void Update()
    {
        if(!isMoveAuto) return;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = currentTarget == startPoint.position ? endPoint.position : startPoint.position;
        }
        if(canFlip)
            Flip();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance && GameManager.Instance.gameState == GameState.Playing)
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }


    bool IsEnemyInFront(Transform player)
    {
        if (transform.localScale.x > 0)
        {
            return transform.position.x < player.position.x;
        }
        else
        {
            return transform.position.x > player.position.x;
        }
    }
    void Flip()
    {
        // Kiểm tra hướng di chuyển của enemy so với target
        if ((currentTarget.x > transform.position.x && !isFacingRight) ||
            (currentTarget.x < transform.position.x && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
