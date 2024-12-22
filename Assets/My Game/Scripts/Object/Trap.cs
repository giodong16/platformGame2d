using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float timeDelay = 1.5f;
    [SerializeField] private float holdTime = 8f;
    public bool isTrap = false;

    [Header("More Spike:")]
    public bool isHadSpike;
    public SingleSpike singleSpike;

    bool isTurnOn = false;
    bool canDestroy = false;
    Animator anim;
    AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTurnOn && isTrap && collision.gameObject.CompareTag("Player"))
        {
            isTurnOn = true;
            canDestroy = true;
            StartCoroutine(TurnOn());
        }
        if (isHadSpike && collision.gameObject.CompareTag("Player"))
        {
            if (singleSpike != null)
            {
                StartCoroutine(singleSpike.StartAttack());
            }
        }
    }

    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(timeDelay);
        if (anim != null) {
            anim.SetBool("isTurnOn", isTurnOn);
        }
        AudioManager.Instance.PlaySFX(audioSource, audioSource.clip);
        yield return new WaitForSeconds(holdTime);
        isTurnOn = false ;
        anim.SetBool("isTurnOn", isTurnOn);
    }
    public void DestroyThis()
    {
        if(canDestroy) Destroy(gameObject);
    }
}
