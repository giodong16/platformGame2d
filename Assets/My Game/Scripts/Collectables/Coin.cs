using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator anim;
    public GameObject effectPlusPrefabs10;
    public GameObject effectPlusPrefabs5;
    private GameObject currentEffect;
    private int plusCoin = 10;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentEffect = effectPlusPrefabs10;
        float random = Random.Range(0f, 1f);
        if (random >= 3)
        {
            plusCoin = 5;
            currentEffect = effectPlusPrefabs5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("Coin_Collect");
            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlaySFX(NameSound.CollectCoin.ToString());
            }
            // Pref.Coins += plusCoin;
            GameManager.Instance?.GetCoins(plusCoin);
            Instantiate(currentEffect,transform.position,Quaternion.identity);
        }
      
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}

