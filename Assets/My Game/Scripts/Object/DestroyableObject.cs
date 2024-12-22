using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    Animator anim;
    bool isBroken = false;
    public GameObject itemPrefabs;

    public bool IsBroken { get => isBroken; set => isBroken = value; }

    public virtual void  Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Broken()
    {
        isBroken = true;
        if(anim != null) 
        anim.SetTrigger("IsBroken");
        if(AudioManager.Instance)
            AudioManager.Instance.PlaySFX(NameSound.WoodBreak.ToString());
        if(itemPrefabs != null)
        {
            Instantiate(itemPrefabs,transform.position,Quaternion.identity);
        }
    }
    public void DestroyThis()
    {
     
        Destroy(gameObject);

    }

}
