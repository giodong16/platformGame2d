using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    PlayerAttack playerAttack;
    public float forceThrown = 5f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        if (GameManager.Instance != null) GameManager.Instance.CurrentCheckPoint = transform.position;
        
    }

    public void TakeDamage(int damage)
    {
        rb.velocity = Vector2.up * forceThrown;
        if (GameManager.Instance != null)
        {
            int oldHeart = GameManager.Instance.HeartForLevel;
            GameManager.Instance.TakeDamage(damage);

            if (GameManager.Instance.HeartForLevel < oldHeart)
            {
                if (anim != null)
                {
                    anim.Play("Death");
                    AudioManager.Instance?.PlaySFX(NameSound.Death.ToString()); 
                    if (GameManager.Instance.HeartForLevel > 0) // Nếu còn hearts, thì hồi sinh
                    {
                        StartCoroutine(DelayRespawn(0.5f));
                    }
                    else
                    {
                        //Destroy(gameObject);
                        gameObject.SetActive(false);
                        if(GameManager.Instance != null)
                        {
                            GameManager.Instance.GameOver();
                        }
                    }
                }
            }
            else
            {
                if (anim != null)
                {
                    anim.Play("Hurt");
                    AudioManager.Instance?.PlaySFX(NameSound.Hurt.ToString());

                }
            }
        }
    }
    void Respawn()
    {
        if (GameManager.Instance != null)
        {
            transform.position = GameManager.Instance.CurrentCheckPoint;

        }
        IdleState();
    }

    private void IdleState()
    {
        if (anim != null) {
            playerAttack.IsCompleted = true;
            anim.Play("Idle");
        }
    }

    IEnumerator DelayRespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn();
    }

}
