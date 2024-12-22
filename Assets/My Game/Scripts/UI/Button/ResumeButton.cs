using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public Button resume;

    private void Start()
    {
        resume = GetComponent<Button>();
        if (resume != null)
        {
            resume.onClick.AddListener(Resume);
        }
    }
    private void Resume()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.Resume();
        }
    }
}
