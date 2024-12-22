using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSpecical : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !SpecicalController01.Instance.IsGameover
            && GameManager.Instance && GameManager.Instance.gameState == GameState.Playing)
        {
            if (SpecicalController01.Instance != null)
                SpecicalController01.Instance.IsGameover = true;
            SpecicalController01.Instance.Complete();
        }
    }
}
