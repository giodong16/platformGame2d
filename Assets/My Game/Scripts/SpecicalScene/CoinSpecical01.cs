using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpecical01 : MonoBehaviour
{
    Animator anim;
    public GameObject effectPlusPrefabs10;
    public GameObject effectPlusPrefabs5;
    private GameObject currentEffect;
    private int plusCoin = 10;
    public float moveSpeed;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentEffect = effectPlusPrefabs10;
        float random = Random.Range(0f, 1f);
        if(random >= 0.3f)
        {
            plusCoin = 5;
            currentEffect = effectPlusPrefabs5;
        }
    }
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
        if (collision.CompareTag("Player"))
        {
            anim.Play("Coin_Collect");
            AudioManager.Instance?.PlaySFX(NameSound.CollectCoin.ToString());
            if (SpecicalController01.Instance) { 
                SpecicalController01.Instance.IncreaseCoin(plusCoin);
            }
            Instantiate(currentEffect,transform.position,Quaternion.identity);
        }
        
      
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}

