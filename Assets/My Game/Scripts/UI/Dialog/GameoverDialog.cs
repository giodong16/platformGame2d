using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverDialog : Dialog
{
    public Text countDownText;
    public Button replayButton;
    public int totalTime =30;
    public int currentTime;
   /* public override void Show(bool isShow)
    {

        currentTime = totalTime;
        UpdateCountDownText();
        base.Show(isShow);
        replayButton.interactable = false;
        if(!isShow) 
            StopCoroutine(TimeDown());
        else
            StartCoroutine(TimeDown());
    }
    public override void Close()
    {
        base.Close();
        currentTime = totalTime;
        StopCoroutine(TimeDown());
    }
    IEnumerator TimeDown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            UpdateCountDownText();
        }

        Pref.Hearts++;
        if (GameManager.Instance)
        {
            GameManager.Instance.CurrentHP = GameManager.Instance.TotalHP;
        }
        if (GUIManager.Instance != null) { 
            GUIManager.Instance.UpdateHeartText();
            GUIManager.Instance.UpdateHeartBar(GameManager.Instance.CurrentHP, GameManager.Instance.TotalHP);
        }
        replayButton.interactable = true;
    }

    public void UpdateCountDownText()
    {
        countDownText.text = currentTime.ToString();
    }*/
}
