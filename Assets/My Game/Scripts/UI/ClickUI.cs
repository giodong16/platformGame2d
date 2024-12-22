using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(()=>Click());
        }
    }
    void Click()
    {   
        if (button == null) return;
        AudioManager.Instance?.PlaySFX(NameSound.UIClick.ToString());
    }
}
