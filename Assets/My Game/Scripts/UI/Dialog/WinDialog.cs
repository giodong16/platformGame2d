using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinDialog : Dialog
{
    public Text levelName;
    public Image[] starsImage;
    public Text coinsText;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (isShow) {
            int starCount = Pref.GetStarsForLevel(Pref.CurrentLevelPlay);
            if (starCount > 0) {
                for (int i = 0; i < starCount; i++) {
                    starsImage[i].color = Color.white;
                }
            }
            levelName.text ="Level "+ Pref.CurrentLevelPlay.ToString("D2");
            SetCoinsText();

        }
    }
    public void SetCoinsText()
    {
        coinsText.text = "Coins: "+ GameManager.Instance?.TotalCoins.ToString();
    }
}
