using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPointsPotion : MonoBehaviour
{
    [SerializeField] int heal =300;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance?.Healing(heal);
            AudioManager.Instance?.PlaySFX(NameSound.CollectKey.ToString());
            Destroy(gameObject);
        }
    }
    
}
