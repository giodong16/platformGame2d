using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Animator anim;
    public Animator UIKeyAnim;
    [SerializeField] private AudioSource audioSource; 
    bool isOpen = false;
    bool isWarning = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isWarning && GameManager.Instance && !GameManager.Instance.IsHadKey && UIKeyAnim != null)
        {
            UIKeyAnim.SetBool("TurnOn", true);
            isWarning = true;
        }
        
        if (!isOpen && collision.CompareTag("Player") && GameManager.Instance != null && GameManager.Instance.IsHadKey)
        {
            isOpen = true;
            anim.SetTrigger("IsOpen");
            AudioManager.Instance?.PlaySFX(audioSource,audioSource.clip);
            if (GameManager.Instance)
            {
                GameManager.Instance.CalculateStarsForLevel();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(GameManager.Instance && !GameManager.Instance.IsHadKey && isWarning && UIKeyAnim != null)
        {
            isWarning = false ;
            UIKeyAnim.SetBool("TurnOn",false);
        }
    }
    public void NextLevel()
    {
        if (GUIManager.Instance) {
            
            GameManager.Instance?.WinLevel();
        }
    }
    
}
