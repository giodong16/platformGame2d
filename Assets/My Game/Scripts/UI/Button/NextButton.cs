using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public Button nextButton;

    private void Start()
    {
        nextButton = GetComponent<Button>();
        if (nextButton == null) return;
        nextButton.onClick.AddListener(NextLevel);
    }

    public void  NextLevel()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.NextLevel();
        }
    }
}
