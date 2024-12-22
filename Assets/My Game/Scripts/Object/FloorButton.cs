using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : Lever
{
    Animator anim;
    AudioSource audioSource;
    private List<GameObject> objectsInTrigger = new List<GameObject>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (anim == null) return;
        anim.SetBool("IsTurnOn", IsTurnOn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || IsTurnOn) return;

        if (!objectsInTrigger.Contains(collision.gameObject))
        {
            objectsInTrigger.Add(collision.gameObject);
        }

        // Nếu nút chưa được bật, kích hoạt nó.
        if (!IsTurnOn)
        {  
            AudioManager.Instance?.PlaySFX(audioSource,audioSource.clip);
            IsTurnOn = true;
            anim.SetBool("IsTurnOn", IsTurnOn);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;

        // Nếu đối tượng có trong danh sách, loại bỏ nó.
        if (objectsInTrigger.Contains(collision.gameObject))
        {
            objectsInTrigger.Remove(collision.gameObject);
        }

        // Nếu danh sách trống, tắt nút.
        if (objectsInTrigger.Count == 0)
        {
            IsTurnOn = false;
            anim.SetBool("IsTurnOn", IsTurnOn);
           
        }
    }
}
