using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    Vector3 currentCheckPoint;
    int totalHP = 300;
    int currentHP;
    int heartForLevel;
    bool isHadKey = false;
    public GameState gameState;

    public int gemForLevel = 0;
    public int toltaGemForLevel = 6;
    private int totalCoins = 0;
    [Header("Require For Each Level:")]
    public List<LevelData> levelDatas;
    public LevelData currentLevelData;

    public Vector3 CurrentCheckPoint { get => currentCheckPoint; set => currentCheckPoint = value; }
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public int TotalHP { get => totalHP; set => totalHP = value; }
    public bool IsHadKey { get => isHadKey; set => isHadKey = value; }
    public int HeartForLevel { get => heartForLevel; set => heartForLevel = value; }
    public int TotalCoins { get => totalCoins; set => totalCoins = value; }

    private  void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); 
        }


        currentHP = totalHP;
        
       
    }

    public void TakeDamage(int damage = 100)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            LoseHeart();
        }
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.UpdateHeartBar(currentHP,totalHP);
        }
      
        
      
    }
    public void LoseHeart()
    {
        //  Pref.Hearts--;
        HeartForLevel--;
        if (HeartForLevel > 0)
        {
            currentHP = totalHP;
            if (GUIManager.Instance != null) {
                GUIManager.Instance.UpdateHeartText();
            }
        }
        else
        {
            currentHP = 0;
            HeartForLevel = 0;
            if (GUIManager.Instance != null)
            {
                GUIManager.Instance.UpdateHeartText(); // Cập nhật lại giao diện số lượng Hearts
            }
           // GameOver();

        }

    }
    public void GetHeart(int heart)
    {
        HeartForLevel++;
        if (GUIManager.Instance != null) {
            GUIManager.Instance.UpdateHeartText();
        }
    }

    public void Healing(int heal)
    {
        currentHP = totalHP;
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.UpdateHeartBar(CurrentHP, totalHP);
        }
    }

    //COIN
    public void GetCoins(int coin)
    {
        //Pref.Coins += coin;
        totalCoins += coin;
        //cập nhật giao diện nếu cần
    }
    public void LoseCoins(int coin)
    {
        Pref.Coins -= coin;
        //cập nhật giao diện nếu cần
    }


    //STATE
    public void GameStart()
    {
        Time.timeScale = 1f;
        if (GUIManager.Instance != null) { 
            GUIManager.Instance.HideCurrentDialog();
        }
        gameState = GameState.Starting;
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.HideCurrentDialog();
        }
        gameState = GameState.Playing;
    }

    public void ShowRequirementAndTasksDialog()
    {
        Time.timeScale = 0f;
        gameState = GameState.Pause;
    }
    public void GamePause()
    {
        Time.timeScale = 0f;
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.ShowGamePause();
        }
        gameState = GameState.Pause;
       
    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlaySFX(NameSound.Gameover.ToString());
        gameState = GameState.Gameover;
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.ShowGameoverDialog();
        }

    }
    public void WinLevel()
    {
        //Time.timeScale = 0f;
        AudioManager.Instance.PlaySFX(NameSound.Win.ToString());
        GUIManager.Instance.winDialog.Show(true);
        Pref.Coins += totalCoins;
        totalCoins = 0;
        if (Pref.CurrentLevelPlay < Pref.MaxLevel)
        {
            Pref.UnlockLevel = Pref.CurrentLevelPlay + 1;
        }
    }

    //funtion 
    public void BackToHome()
    {
      
        SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
        if (sceneTransition != null)
        {
            sceneTransition.ChangeScene("HomeScene");
        }
        else
        {
            SceneManager.LoadScene("HomeScene");
        }
        GameStart();

    }
    public void Resume()
    {
        PlayGame();
       
    }
    public void Replay()
    {
            PlayGame();
            string currentSceneName = SceneManager.GetActiveScene().name; 
            SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
            if (sceneTransition != null)
            {
                sceneTransition.ChangeScene(currentSceneName);
            }
            else
            {
                SceneManager.LoadScene(currentSceneName);
            }
       
    }
  
    public void OpenLevel(int level)
    {
        // check level đặc biệt
        if (Pref.IsSpecialLevel(level) && Pref.IsSpecialLevelCompleted(level))
        {
            if (HomeGUIManger.Instance != null)
            {
                HomeGUIManger.Instance.CreateMessage("Message", "This special level has already been completed and cannot be played again!");
            }
            else if (GUIManager.Instance != null)
            {
                GUIManager.Instance.CreateMessage("Message", "This special level has already been completed and cannot be played again!");
            }
            return;
        }

        //check Stars 
        if (level % 5 == 0 && Pref.GetTotalStars(1,level) < (level-1)*3-1 ) {
            if (HomeGUIManger.Instance != null)
            {
                HomeGUIManger.Instance.CreateMessage("Message", "Not enough stars to unlock level "+ level+"!");
            }
            else
            if (GUIManager.Instance != null) {
                GUIManager.Instance.CreateMessage("Message", "Not enough stars to unlock level" + level + "!");
            }
        }
        else
        {
            string levelName = "Level" + (level).ToString("D2");
            Pref.CurrentLevelPlay = level;
            SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
            if (sceneTransition != null)
            {
                sceneTransition.ChangeScene(levelName);
            }
            else
            {
                SceneManager.LoadScene(levelName);
            }
        }
       
    }
    public void NextLevel()
    {
            PlayGame();
            if (Pref.CurrentLevelPlay < Pref.MaxLevel)
            {
                Pref.CurrentLevelPlay++;
                OpenLevel(Pref.CurrentLevelPlay);
            }
            else
            {
                GUIManager.Instance?.CreateMessage("Message", "You have completed this game!");
            }
    }
    public void CalculateStarsForLevel()
    {
        int currentLevel = Pref.CurrentLevelPlay;
        if(gemForLevel == toltaGemForLevel)
        {
            Pref.SaveStarsForLevel(currentLevel,3);
        }
        else if( gemForLevel < toltaGemForLevel && gemForLevel>=toltaGemForLevel*2/3 )
        {
            Pref.SaveStarsForLevel(currentLevel, 2);
        }
        else
        {
            Pref.SaveStarsForLevel(currentLevel, 1);
        }
        gemForLevel = 0;
    }

    //LEVEL
    public void GetCurrentLevelData() {    
        if(levelDatas == null || levelDatas.Count <=0) return;
        int levelDataIndex = levelDatas.FindIndex(x => x.levelNumber == Pref.CurrentLevelPlay);
        if (levelDataIndex < 0) return;
        currentLevelData =  levelDatas[levelDataIndex];
    }
    public void SetHeartForLevel()
    {
        GetCurrentLevelData();
        if (currentLevelData != null) {
            HeartForLevel = currentLevelData.levelHearts;
        }
    }
    public void ResetData()
    {
        currentHP = totalHP;
        toltaGemForLevel = 0;
        gemForLevel=0;
        totalCoins = 0;
    }

}
