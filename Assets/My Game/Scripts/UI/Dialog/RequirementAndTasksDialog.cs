using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementAndTasksDialog : Dialog
{
    public Text requirementAndTasksText;
/*    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }*/
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (isShow)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetHeartForLevel();
              
                SetUpUI(GameManager.Instance.currentLevelData);

                GameManager.Instance.ShowRequirementAndTasksDialog();
            }
       //     Invoke("Appear", 2f);
           
        }
    }
    public void SetUpUI(LevelData levelData)
    {
        if (levelData == null) { return; }
        requirementAndTasksText.text = "";
        foreach (string content in levelData.tasksAndRequirements)
        {
            requirementAndTasksText.text += content + "\n";
        }
    }
    public override void Close()
    {
        base.Close();
        GameManager.Instance?.PlayGame();
        GUIManager.Instance.UpdateHeartText();


    }
/*    public void Appear()
    {
        if (anim != null)
        {
            anim.SetBool("IsAppear", true);
        }
    }*/
}
