using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpike : MonoBehaviour
{
    public int damage = 150;
    public Animator anim;
    public float timeDelay = 0.5f;
    public bool isAttacking = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
    public IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(timeDelay);
        isAttacking = true;
        if (anim != null) {
            anim.SetTrigger("Run");
        }
    }
    public void ResetAttack()
    {
        isAttacking = false;
        if (anim != null)
        {
            anim.ResetTrigger("Run");
        }

        anim.Play("Idle");

    }
}
