using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyAutoDestroy : MonoBehaviour
{
    PlayerController player;
    bool isRun = false;
    public bool isAttack;
    private bool isFacingRight = false;
    Animator anim;

    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRun = true;
            if(anim != null) anim.SetBool("IsRun",isRun);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(300);
        }
    }
    public void LookAtPlayer()
    {
       
        Vector3 localScale = transform.localScale;
        if (transform.position.x > player.transform.position.x && isFacingRight)
        {
            IsFacingRight = !IsFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if(transform.position.x < player.transform.position.x && !isFacingRight)
        {
            IsFacingRight = !IsFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        } 
       
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }


}
