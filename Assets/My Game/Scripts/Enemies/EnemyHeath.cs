using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeath : MonoBehaviour
{
    public int maxHeath = 2;
    public GameObject HPPotionPrefab;
    [SerializeField]int currentHeath;
    Animator anim; 
    private void Start()
    {
        currentHeath = maxHeath;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage = 1)
    {
        currentHeath -= damage;
        if (currentHeath <= 0)
        {
            if (anim != null) {
                anim.Play("Die");
            }
            else
            {
                DestroyThis();
            }
        }
    }

    public void DestroyThis()
    {
        if (HPPotionPrefab !=null) {
            float index = Random.Range(0f, 100f);
            if (index < 20f) {
                Instantiate(HPPotionPrefab, transform.position,Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
