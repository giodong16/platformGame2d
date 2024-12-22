using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToHomeButton : MonoBehaviour
{
    public Button backToHome;

    private void Start()
    {
        backToHome = GetComponent<Button>();
        if (backToHome != null)
        {
            backToHome.onClick.AddListener(BackToHome);
        }
    }
    private void BackToHome()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.BackToHome();
        }
    }
}
