using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Button pause;

    private void Start()
    {
        pause = GetComponent<Button>(); 
        if (pause != null)
        {
            pause.onClick.AddListener( OpenPauseDialog);
        }
    }
    private void OpenPauseDialog()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.GamePause();
        }
    }
}
