using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 150;
    public int enragedAttackDamage = 300;

    public bool isAngry;

    public Transform attackPoint;
    public float attackRange = 0.65f;
    public LayerMask attackMask;

    public void Attack()
    {
        if(attackPoint != null)
        {
            Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackMask);
            if (colInfo != null)
            {
                PlayerController player = colInfo.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(attackDamage);
                }
            }
        }
       

    }

    public void EnragedAttack()
    {
        if (attackPoint != null) {
            Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackMask);
            if (colInfo != null)
            {
                PlayerController player = colInfo.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(enragedAttackDamage);
                }
            }
        }

       
    }

/*    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/

}