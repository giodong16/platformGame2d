using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public Button[] tabButtons;
    public GameObject[] tabContents;

    Button activeTabButton;
    GameObject activeTabContent;
    private void Start()
    {
        for (int i = 0; i < tabButtons.Length; i++) {
            int index = i;
            tabButtons[i].onClick.AddListener(() => OnTabClick(tabButtons[index], tabContents[index]));
        }
        OnTabClick(tabButtons[0], tabContents[0]);
    }
    void OnTabClick(Button button,GameObject tabContent)
    {
        if (activeTabButton != null)
        {
            ResetTabButtonColor(activeTabButton);
            if(activeTabContent != null)
            {
                activeTabContent.SetActive(false);
            }
        }
      
        SetActiveTabButtonColor(button);
        tabContent.SetActive(true);

        activeTabButton = button;
        activeTabContent = tabContent;

    }

    void SetActiveTabButtonColor(Button button)
    {
        Color activeColor;
        if (ColorUtility.TryParseHtmlString("#1E283C",out activeColor))
        {
            button.GetComponent<Image>().color = activeColor;
        }
    }
    void ResetTabButtonColor(Button button) {

        Color normalColor;
        if (ColorUtility.TryParseHtmlString("#101628", out normalColor))
        {
            button.GetComponent<Image>().color = normalColor;
        }
    }

}
