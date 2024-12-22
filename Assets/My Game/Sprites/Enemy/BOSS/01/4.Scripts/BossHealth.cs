using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public GameObject awards;
    public GameObject key;
    public GameObject door;
    public Image fillHealth;
    public GameObject healthBar;

    public int totalHealth = 30;

    int currentHealth;
    public GameObject deathEffect;

    public bool isInvulnerable = false;

    private void Start()
    {
        currentHealth = totalHealth;
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0.4 *totalHealth )
        {
            GetComponent<Animator>().SetBool("IsAngry", true);
        }

        if (currentHealth <= 0)
        {
            //GetComponent<Animator>().Play("Die");
            Die();
        }
    }

    void Die()
    {
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (awards != null) { 
            awards.SetActive(true);
        }
        if (key != null) { 
            key.SetActive(true);
        }
        if(door != null)
        {
            door.SetActive(true);
        }
        if (healthBar != null) {
            healthBar.SetActive(false);
        }
        
        Destroy(gameObject);
    }
    void UpdateHealthBar()
    {
        fillHealth.fillAmount = (float)currentHealth/totalHealth;
    }

}