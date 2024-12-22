using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPraticle : MonoBehaviour
{
    [SerializeField] ParticleSystem keyParticle;

   /* [Range(0, 10)]
    [SerializeField] int occurAfterVelcity;*/

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;
    [SerializeField] Rigidbody2D rb;
    float counter;
    bool isCollected = false;


  
    private void Update()
    {
        
        counter += Time.deltaTime;
        if (isCollected)
        {
            if (counter > dustFormationPeriod)
            {
                keyParticle.Play();
                counter = 0;
            }
        }
            
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected) 
        {
            isCollected = true;
           
        }
    }
}
