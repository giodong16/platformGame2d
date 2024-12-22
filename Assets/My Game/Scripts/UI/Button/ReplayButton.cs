using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    public Button replay;

    private void Start()
    {
        replay = GetComponent<Button>();
        if (replay != null)
        {
            replay.onClick.AddListener(Replay);
        }
    }
    private void Replay()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.Replay();
        }
    }
}
