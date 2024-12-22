using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpecical01 : MonoBehaviour
{
    public float moveSpeed;
    
    private void FixedUpdate()
    {
        transform.position = transform.position + Vector3.left * moveSpeed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {   
            Destroy(gameObject);
        }
    }
}
