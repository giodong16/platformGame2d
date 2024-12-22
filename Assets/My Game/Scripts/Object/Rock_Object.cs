using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Object : MonoBehaviour
{
    Rigidbody2D rb;
    bool isPlayingLoopSFX = false;
    AudioSource audioSource;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Mathf.Abs(rb.velocity.x )> 0.1f)
        {
            if (!isPlayingLoopSFX)
            {
                AudioManager.Instance?.PlaySFXLoop(audioSource,audioSource.clip);
                isPlayingLoopSFX = true;
            }

        }
        else
        {
            if (isPlayingLoopSFX)
            {
                if (audioSource != null)
                {
                    audioSource.Stop();
                }
                isPlayingLoopSFX = false;
            }
        }

    }
}

