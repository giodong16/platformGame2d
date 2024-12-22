using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelcity; 

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;
    [SerializeField] Rigidbody2D playerRb;
    float counter;
    bool isGrounded;


    [SerializeField] ParticleSystem fallParticle;
    private void Update()
    {
        counter += Time.deltaTime;
        if (isGrounded && Mathf.Abs(playerRb.velocity.x) > occurAfterVelcity) {
            if (counter > dustFormationPeriod) { 
                movementParticle.Play();
                counter = 0;    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Rock"))
        {
            fallParticle.Play();
            isGrounded = true;
            if (AudioManager.Instance != null )
            {
                AudioManager.Instance.PlaySFX(NameSound.Land.ToString());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
