using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    Animator anim;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlaySFX(NameSound.CollectKey.ToString());
            }
            if (GameManager.Instance)
            {
                GameManager.Instance.gemForLevel++;
                GUIManager.Instance.UpdateGemsText();
            }
            Destroy(gameObject);  
        }

    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
