using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Animator anim;
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public float throwForce = 6f;
    PlayerMovement playerMovement;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    public void ResetBow()
    {
        SpawnArrow();
        anim.Play("InActive");
    }
    public void SpawnArrow()
    {
        if (arrowPrefab != null)
        {
            GameObject arrowClone = Instantiate(arrowPrefab, spawnPoint.position, Quaternion.identity);
            //stoneClone.SetVelocityX(transform.localScale.x *throwForce);
            Vector3 localScale = arrowClone.transform.localScale;
            localScale.x = playerMovement.transform.localScale.x;
            arrowClone.transform.localScale = localScale;
            Rigidbody2D rb = arrowClone.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(playerMovement.transform.localScale.x * throwForce, rb.velocity.y);
        }
    }
}
