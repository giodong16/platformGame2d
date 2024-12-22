using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelMenu : Dialog
{
    public LevelButtonUI levelButtonPrefabs;
    private LevelButtonUI[] levelButtons;
    public int levelCount;
    public GameObject gridRoot;

    private void Start()
    {
        levelButtons = new LevelButtonUI[levelCount];
        CreateData();
        SetActiveLevelButton();
    }
    void SetActiveLevelButton()
    {
        if (levelButtons == null) return;
            int unlockedLevel = Pref.UnlockLevel;
        for (int i = 0; i < levelCount; i++)
        {

            bool isInteractable = i < unlockedLevel;

            // Kiểm tra nếu là level đặc biệt và đã hoàn thành thì vô hiệu hóa nó
            if (Pref.IsSpecialLevel(i + 1) && Pref.IsSpecialLevelCompleted(i + 1))
            {
                isInteractable = false;
            }

            levelButtons[i].levelButton.interactable = isInteractable;
            /* if(i >= unlockedLevel)
             levelButtons[i].levelButton.interactable  = false;
             else
                 levelButtons[i].levelButton.interactable = true;*/

        }
    }
    void CreateData()
    {
        if (levelButtons == null || gridRoot == null || levelButtons.Length <= 0 || levelButtonPrefabs == null) return;

        for (int i = 0; i < levelCount; i++)
        {
            int index = i;

            // Tạo instance mới của prefab cho mỗi button
            LevelButtonUI newButton = Instantiate(levelButtonPrefabs,gridRoot.transform);

            // Gán cho mảng
            levelButtons[i] = newButton;

            // Đặt Id, tên cho button , hiển thị số sao
            newButton.Id = index + 1;
            newButton.levelButton.name = "Level" + (index + 1).ToString("D2");
            
            if(index < Pref.UnlockLevel)
            {
                int stars = Pref.GetStarsForLevel(index + 1);
                for (int st = 0; st < stars; st++)
                {
                    newButton.starImage[st].color = Color.white;
                }
            }
            else
            {
                for (int st = 0; st < 3; st++)
                {
                    newButton.starImage[st].enabled = false;
                }
            }
            
            Text contentText = newButton.levelButton.GetComponentInChildren<Text>();
            if (contentText != null)
            {
                contentText.text = (index + 1).ToString("D2");
            }
          
            newButton.levelButton.onClick.AddListener(() => OpenLevel(index+1));
        }
    }



    public void OpenLevel(int levelId )
    {
            GameManager.Instance.OpenLevel(levelId);
          //  Pref.CurrentLevelPlay = levelId;
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);

    }
    public void OpenMaxLevel()
    {
        GameManager.Instance.OpenLevel(Pref.UnlockLevel);
       // Pref.CurrentLevelPlay = Pref.UnlockLevel;
        
    }
}
