using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemyRun : StateMachineBehaviour
{
    Transform playerTransform;
    Rigidbody2D rb;
    public float attackRange = 1.5f;
    AIEnemyAutoDestroy AIBoom;
    CircleCollider2D circleCollider;
    public float speed = 3f;

   
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        AIBoom = animator.GetComponent<AIEnemyAutoDestroy>();
        circleCollider = animator.GetComponent<CircleCollider2D>();
    }

  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AIBoom.LookAtPlayer();
        Vector2 target = new Vector2(playerTransform.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
        if (Vector2.Distance(playerTransform.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("IsAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


}
