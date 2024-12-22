using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPab : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] private AudioSource audioSource;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("Run");
            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlaySFX(audioSource,audioSource.clip );
            }
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Kiểm tra nếu nhân vật đang nhảy
                if (rb.velocity.y != 0) // Đang nhảy 
                {
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, force));
                }
                else
                {
                    rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                }
            }
        }
    }
    
    void Idle()
    {
        anim.Play("Idle");
    }
    
}
