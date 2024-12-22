using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyOnGround : MonoBehaviour
{
    float speed = 2f;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Vector3 currentTarget;
    public int damage =100;
    Animator anim;
    bool isFacingRight = true;
    

    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentTarget = endPoint.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = currentTarget == startPoint.position ? endPoint.position : startPoint.position;
        }
        Flip();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsEnemyInFront(collision.transform))
            { 
                collision.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
    }


    bool IsEnemyInFront(Transform player)
    {
        if(transform.localScale.x> 0)
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
