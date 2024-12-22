using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lamppost : MonoBehaviour
{
    public GameObject fire;
    public Animator anim;
    [SerializeField] AudioSource audioSource;
    bool isOpen =false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpen)
        {
            if (fire != null)
            {
                anim.Play("StartFire");
                
            }
            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlaySFX(audioSource,audioSource.clip);
            }

            isOpen = true;
            anim.SetBool("IsOpen",isOpen);
            if (GameManager.Instance != null) { 
                GameManager.Instance.CurrentCheckPoint = fire.transform.position;
            }
           
        }
    
    }

}
