using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class LeverControl : Lever
{
    [SerializeField] Animator anim;
    AudioSource audioSource;
    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if(anim != null ) 
            anim.SetBool("IsTurnOn",IsTurnOn);
      //  anim.Play("Off");
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player.HorizontalMovement != 0)
            {
                if (AudioManager.Instance)
                {
                    AudioManager.Instance?.PlaySFX(audioSource, audioSource.clip);
                }

                if (player.HorizontalMovement > 0)
                {   
                    IsTurnOn = true;
                    anim.SetBool("IsTurnOn", IsTurnOn);
                }
                else
                {
                    IsTurnOn = false;
                    anim.SetBool("IsTurnOn", IsTurnOn);
                    
                }
                
            }
   
        }
    }
   

}
