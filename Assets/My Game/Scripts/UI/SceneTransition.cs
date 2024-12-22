using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float timeDelay = 3.8f;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ChangeScene(string  levelName)
    {
      //  if (Pref.Hearts <= 0 && levelName!="HomeScene") return;
        StartCoroutine(OpenTransition(levelName));
    }

    IEnumerator OpenTransition(string levelName)
    {  
        gameObject.SetActive(true);
        if (anim != null)
        {
            anim.SetTrigger("OpenTransition");
        }
        yield return new WaitForSeconds(timeDelay);
      
       
        SceneManager.LoadScene(levelName);
    }
}
