using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;

    public bool IsFacingRight = false;
   
    public void LookAtPlayer()
    {
        if(player == null) return;
        Vector3 localScale = transform.localScale;
        if (transform.position.x > player.transform.position.x && IsFacingRight)
        {
            IsFacingRight = !IsFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if (transform.position.x < player.transform.position.x && !IsFacingRight)
        {
            IsFacingRight = !IsFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
/*    void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }*/

}