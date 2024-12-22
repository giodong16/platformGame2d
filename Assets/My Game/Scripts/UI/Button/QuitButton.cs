using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Quit);
    }
    void Quit()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
        // Nếu đang chạy trên các nền tảng khác (PC, Windows)
        Application.Quit();
    #endif
    }
}